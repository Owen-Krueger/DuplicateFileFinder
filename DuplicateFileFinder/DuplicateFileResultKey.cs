namespace DuplicateFileFinder;

public record DuplicateFileResultKey(string Key, string DisplayKey)
{
    public override string ToString() => DisplayKey;
}