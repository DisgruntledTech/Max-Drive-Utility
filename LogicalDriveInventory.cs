using System;
using System.Runtime.Versioning;
using System.Management;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Retrieves logical drive information using WMI.
/// </summary>
[SupportedOSPlatform("windows")]
public class LogicalDriveInventory
{
    public class LogicalDrive
    {
        public required string DriveLetter { get; set; }
        public required string FileSystem { get; set; }
        public ulong TotalSize { get; set; }
        public ulong FreeSpace { get; set; }
        public ulong UsedSpace { get; set; }
        public double PercentUsed { get; set; }
        public required string DriveType { get; set; }
        public required string VolumeName { get; set; }
    }

    /// <summary>
    /// Gets all logical drives on the system.
    /// </summary>
    /// <returns>List of LogicalDrive objects</returns>
    public static List<LogicalDrive> GetLogicalDrives()
    {
        List<LogicalDrive> drives = new List<LogicalDrive>();

        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Storage device retrieval is only supported on Windows.");
            return drives;
        }

        try
        {
            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2");
            scope.Connect();

            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_LogicalDisk WHERE DriveType=3");
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject drive in searcher.Get())
            {
                ulong totalSize = Convert.ToUInt64(drive["Size"] ?? 0);
                ulong freeSpace = Convert.ToUInt64(drive["FreeSpace"] ?? 0);
                ulong usedSpace = totalSize - freeSpace;

                LogicalDrive logicalDrive = new LogicalDrive
                {
                    DriveLetter = drive["Name"]?.ToString() ?? "Unknown",
                    FileSystem = drive["FileSystem"]?.ToString() ?? "Unknown",
                    TotalSize = totalSize,
                    FreeSpace = freeSpace,
                    UsedSpace = usedSpace,
                    PercentUsed = totalSize > 0 ? (double)usedSpace / totalSize * 100 : 0,
                    DriveType = GetDriveType(Convert.ToInt32(drive["DriveType"] ?? 0)),
                    VolumeName = drive["VolumeName"]?.ToString() ?? "(No Name)"
                };

                drives.Add(logicalDrive);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving logical drives: {ex.Message}");
        }
        return drives;
    }

    /// <summary>
    /// Converts WMI drive type code to readable string.
    /// </summary>
    private static string GetDriveType(int driveType)
    {
        return driveType switch
        {
            1 => "No Root Directory",
            2 => "Removable Disk",
            3 => "Local Disk",
            4 => "Network Drive",
            5 => "CD-ROM Drive",
            6 => "RAM Disk",
            _ => "Unknown"
        };
    }
}
