# Max Drive Utility

A comprehensive C# console application for inventorying storage devices on Windows systems using Windows Management Instrumentation (WMI).

## Features

✅ **Physical Drive Information**
- Device name and model
- Serial numbers
- Interface type (SATA, NVMe, etc.)
- Total capacity
- Media type (SSD, HDD, etc.)
- Device status

✅ **Logical Drive Analysis**
- Drive letters and volume names
- File system type
- Total, used, and free space
- Usage percentage
- Drive type classification

✅ **Partition Details**
- Partition names and indices
- Partition size and type
- Boot partition identification
- Starting offset information

## Requirements

- **Operating System**: Windows (XP or later)
- **.NET Runtime**: .NET 6.0 or higher
- **Privileges**: Administrator/Elevated privileges required for full WMI access

## Getting Started

### Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- Visual Studio 2019+ or Visual Studio Code

### Building the Project

```bash
# Clone the repository
git clone https://github.com/DisgruntledTech/Max-Drive-Utility.git
cd Max-Drive-Utility

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Build for release
dotnet build --configuration Release
```

### Running the Application

**Important**: Run as Administrator for full functionality

```bash
# Debug mode
dotnet run

# Release mode
dotnet run --configuration Release
```

**Or run the compiled executable directly:**
```bash
./bin/Release/net6.0/MaxDriveUtility.exe
```

## Project Structure

```
Max-Drive-Utility/
├── StorageInventory.cs          # Physical storage devices WMI queries
├── LogicalDriveInventory.cs     # Logical drive WMI queries
├── PartitionInventory.cs        # Disk partition WMI queries
├── Program.cs                   # Main console application
├── Max-Drive-Utility.csproj     # Project configuration
├── App.manifest                 # Admin privilege manifest
├── .gitignore                   # Git ignore rules
└── README.md                    # This file
```

## Code Examples

### Getting Physical Storage Devices
```csharp
var devices = StorageInventory.GetStorageDevices();
foreach (var device in devices)
{
    Console.WriteLine($"Device: {device.Model}");
    Console.WriteLine($"Size: {device.Size} bytes");
}
```

### Getting Logical Drives
```csharp
var drives = LogicalDriveInventory.GetLogicalDrives();
foreach (var drive in drives)
{
    Console.WriteLine($"Drive: {drive.DriveLetter}");
    Console.WriteLine($"Free Space: {drive.FreeSpace} bytes");
}
```

### Getting Partitions
```csharp
var partitions = PartitionInventory.GetPartitions();
foreach (var partition in partitions)
{
    Console.WriteLine($"Partition: {partition.PartitionName}");
    Console.WriteLine($"Size: {partition.Size} bytes");
}
```

## Classes and Methods

### StorageInventory
- `GetStorageDevices()` - Returns list of physical storage devices
- Properties: DeviceName, Model, SerialNumber, InterfaceType, Size, Status, MediaType

### LogicalDriveInventory
- `GetLogicalDrives()` - Returns list of logical drives
- Properties: DriveLetter, FileSystem, TotalSize, FreeSpace, UsedSpace, PercentUsed, DriveType, VolumeName

### PartitionInventory
- `GetPartitions()` - Returns list of disk partitions
- Properties: PartitionName, DiskIndex, IndexNumber, Size, StartingOffset, Type, BootPartition

## WMI Classes Used

- **Win32_DiskDrive** - Physical storage devices
- **Win32_LogicalDisk** - Logical drives
- **Win32_DiskPartition** - Disk partitions

## Error Handling

The application includes try-catch blocks for all WMI queries. Errors are logged to the console and the application continues execution.

## Permissions

The `App.manifest` file requests administrator privileges automatically when the application runs. Some WMI queries may require elevated permissions to access all device information.

## Performance Notes

- Initial execution may take a few seconds while WMI performs queries
- Results can be cached if querying frequently to reduce overhead
- WMI queries are generally safe and non-destructive

## Troubleshooting

**"Access Denied" errors**
- Run the application as Administrator
- Check that WMI service is running on your system

**"WMI Connection Failed" errors**
- Ensure Windows Management Instrumentation service is enabled
- Try restarting the WMI service: `net stop winmgmt` / `net start winmgmt`

**No devices found**
- Ensure you're running with Administrator privileges
- Check that the system has storage devices connected

## License

This project is open source and available under the MIT License.

## Contributing

Contributions are welcome! Feel free to submit pull requests or open issues for bugs and feature requests.

## Support

For issues, questions, or suggestions, please open an issue on the GitHub repository.

---

**Max Drive Utility** - Comprehensive Storage Device Inventory for Windows Systems