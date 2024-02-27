using System.IO;
using System.Windows.Controls;

namespace DuplicateFileFinder;

public partial class ProgressUserControl : UserControl
{
    private readonly string directory;
    
    public ProgressUserControl(string directory)
    {
        this.directory = directory;
        InitializeComponent();
    }

    public Dictionary<string, List<FileEntry>> GetDuplicateFiles()
    {
        var fileEntries = GetFileEntriesFromDirectory(directory);
        return fileEntries
            .GroupBy(x => new { x.FileName, x.FileSize })
            .Where(x => x.Count() > 1)
            .ToDictionary(x => $"{x.Key.FileName}_{x.Key.FileSize}", x => x.ToList());
    }

    private static List<FileEntry> GetFileEntriesFromDirectory(string directory)
    {
        Console.WriteLine(directory);
        var files = Directory.GetFiles(directory)
            .Select(x => new FileInfo(x))
            .Select(x => new FileEntry(x.FullName, x.Length))
            .ToList();

        foreach (var subDirectory in Directory.GetDirectories(directory))
        {
            files.AddRange(GetFileEntriesFromDirectory(subDirectory));
        }

        return files;
    }
}