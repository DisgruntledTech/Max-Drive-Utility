using System;
using System.IO;

public class ArchiveFiles
{
    public static void ArchiveFilesInDirectory(string sourceDirectory, string archiveDirectory)
    {
        try
        {
            if (!Directory.Exists(sourceDirectory))
            {
                Console.WriteLine($"Source directory '{sourceDirectory}' does not exist.");
                return;
            }

            if (!Directory.Exists(archiveDirectory))
            {
                Directory.CreateDirectory(archiveDirectory);
            }

            var files = Directory.GetFiles(sourceDirectory);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var destinationPath = Path.Combine(archiveDirectory, fileName);

                File.Move(file, destinationPath);
                Console.WriteLine($"Archived: {fileName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error archiving files: {ex.Message}");
        }
    }
}   
