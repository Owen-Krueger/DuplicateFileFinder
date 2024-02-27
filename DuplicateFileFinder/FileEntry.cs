using System.IO;

namespace DuplicateFileFinder;

public record FileEntry(string FilePath, long FileSize)
{
    public string FileName => Path.GetFileName(FilePath);

    public override string ToString()
    {
        return FilePath;
    }
}