using System;
using System.Management;
using System.Collections.Generic;

/// <summary>
/// Retrieves disk partition information using WMI.
/// </summary>
public class PartitionInventory
{
    public class Partition
    {
        public string PartitionName { get; set; }
        public string DiskIndex { get; set; }
        public string IndexNumber { get; set; }
        public ulong Size { get; set; }
        public ulong StartingOffset { get; set; }
        public string Type { get; set; }
        public bool BootPartition { get; set; }
    }

    /// <summary>
    /// Gets all disk partitions on the system.
    /// </summary>
    /// <returns>List of Partition objects</returns>
    public static List<Partition> GetPartitions()
    {
        List<Partition> partitions = new List<Partition>();

        try
        {
            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2");
            scope.Connect();

            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_DiskPartition");
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject partition in searcher.Get())
            {
                Partition part = new Partition
                {
                    PartitionName = partition["Name"]?.ToString() ?? "Unknown",
                    DiskIndex = partition["DiskIndex"]?.ToString() ?? "Unknown",
                    IndexNumber = partition["Index"]?.ToString() ?? "Unknown",
                    Size = Convert.ToUInt64(partition["Size"] ?? 0),
                    StartingOffset = Convert.ToUInt64(partition["StartingOffset"] ?? 0),
                    Type = partition["Type"]?.ToString() ?? "Unknown",
                    BootPartition = Convert.ToBoolean(partition["BootPartition"] ?? false)
                };

                partitions.Add(part);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving partitions: {ex.Message}");
        }
        return partitions;
    }
}