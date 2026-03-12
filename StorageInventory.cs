using System;
using System.Management;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Retrieves physical storage device information using WMI.
/// </summary>
public class StorageInventory
{
    public class StorageDevice
    {
        public required string DeviceName { get; set; }
        public required string Model { get; set; }
        public required string SerialNumber { get; set; }
        public required string InterfaceType { get; set; }
        public ulong Size { get; set; }
        public required string Status { get; set; }
        public required string MediaType { get; set; }
    }

    /// <summary>
    /// Gets all physical storage devices on the system.
    /// </summary>
    /// <returns>List of StorageDevice objects</returns>
    public static List<StorageDevice> GetStorageDevices()
    {
        List<StorageDevice> devices = new List<StorageDevice>();
        
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Storage device retrieval is only supported on Windows.");
            return devices;
        }

        try
        {
            ManagementScope scope = new ManagementScope(@"\\.\root\cimv2");
            scope.Connect();

            // Query physical drives
            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_DiskDrive");
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject device in searcher.Get())
            {
                StorageDevice storage = new StorageDevice
                {
                    DeviceName = device["Name"]?.ToString() ?? "Unknown",
                    Model = device["Model"]?.ToString() ?? "Unknown",
                    SerialNumber = device["SerialNumber"]?.ToString() ?? "Unknown",
                    InterfaceType = device["InterfaceType"]?.ToString() ?? "Unknown",
                    Size = Convert.ToUInt64(device["Size"] ?? 0),
                    Status = device["Status"]?.ToString() ?? "Unknown",
                    MediaType = device["MediaType"]?.ToString() ?? "Unknown"
                };

                devices.Add(storage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving storage devices: {ex.Message}");
        }

        return devices;
    }
}
