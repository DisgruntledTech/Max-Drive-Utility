using System;
using System.Collections.Generic;

/// <summary>
/// Main application for displaying storage device inventory.
/// </summary>
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   MAX DRIVE UTILITY - Storage Inventory  ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        // Physical Drives
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("PHYSICAL STORAGE DEVICES");
        Console.WriteLine("─────────────────────────────────────────");
        var physicalDrives = StorageInventory.GetStorageDevices();
        
        if (physicalDrives.Count == 0)
        {
            Console.WriteLine("No physical drives found.");
        }
        else
        {
            foreach (var drive in physicalDrives)
            {
                Console.WriteLine($"\n  Device:        {drive.DeviceName}");
                Console.WriteLine($"  Model:         {drive.Model}");
                Console.WriteLine($"  Serial Number: {drive.SerialNumber}");
                Console.WriteLine($"  Interface:     {drive.InterfaceType}");
                Console.WriteLine($"  Size:          {FormatBytes(drive.Size)}");
                Console.WriteLine($"  Media Type:    {drive.MediaType}");
                Console.WriteLine($"  Status:        {drive.Status}");
            }
        }

        // Logical Drives
        Console.WriteLine("\n\n─────────────────────────────────────────");
        Console.WriteLine("LOGICAL DRIVES");
        Console.WriteLine("─────────────────────────────────────────");
        var logicalDrives = LogicalDriveInventory.GetLogicalDrives();
        
        if (logicalDrives.Count == 0)
        {
            Console.WriteLine("No logical drives found.");
        }
        else
        {
            foreach (var drive in logicalDrives)
            {
                Console.WriteLine($"\n  Drive Letter:  {drive.DriveLetter}");
                Console.WriteLine($"  Volume Name:   {drive.VolumeName}");
                Console.WriteLine($"  File System:   {drive.FileSystem}");
                Console.WriteLine($"  Drive Type:    {drive.DriveType}");
                Console.WriteLine($"  Total Size:    {FormatBytes(drive.TotalSize)}");
                Console.WriteLine($"  Used Space:    {FormatBytes(drive.UsedSpace)} ({drive.PercentUsed:F2}%)");
                Console.WriteLine($"  Free Space:    {FormatBytes(drive.FreeSpace)}");
            }
        }

        // Partitions
        Console.WriteLine("\n\n─────────────────────────────────────────");
        Console.WriteLine("DISK PARTITIONS");
        Console.WriteLine("─────────────────────────────────────────");
        var partitions = PartitionInventory.GetPartitions();
        
        if (partitions.Count == 0)
        {
            Console.WriteLine("No partitions found.");
        }
        else
        {
            foreach (var partition in partitions)
            {
                Console.WriteLine($"\n  Partition:     {partition.PartitionName}");
                Console.WriteLine($"  Disk Index:    {partition.DiskIndex}");
                Console.WriteLine($"  Index Number:  {partition.IndexNumber}");
                Console.WriteLine($"  Size:          {FormatBytes(partition.Size)}");
                Console.WriteLine($"  Type:          {partition.Type}");
                Console.WriteLine($"  Boot Partition:{(partition.BootPartition ? " Yes" : " No")}");
            }
        }

        Console.WriteLine("\n\n─────────────────────────────────────────");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    /// <summary>
    /// Formats byte values into human-readable format.
    /// </summary>
    static string FormatBytes(ulong bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}