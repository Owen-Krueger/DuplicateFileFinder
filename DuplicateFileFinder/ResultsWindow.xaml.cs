using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DuplicateFileFinder;

public partial class ResultsWindow : Window
{
    private readonly IReadOnlyDictionary<string, List<FileEntry>> duplicateFiles;
    
    public ResultsWindow(IReadOnlyDictionary<string, List<FileEntry>> duplicateFiles)
    {
        InitializeComponent();
        this.duplicateFiles = duplicateFiles;

        ListFileNames.ItemsSource = this.duplicateFiles.Select(x => new DuplicateFileResultKey(x.Key, x.Value.FirstOrDefault()?.FileName ?? x.Key));
    }

    private void ListFileNames_OnSelected(object sender, SelectionChangedEventArgs e)
    {
        if (ListFileNames.SelectedItem is not DuplicateFileResultKey selected)
        {
            return;
        }

        ListFileDetails.ItemsSource = duplicateFiles[selected.Key];
    }

    private void ListFileDetails_OnDoubleClicked(object sender, MouseButtonEventArgs e)
    {
        if (ListFileDetails.SelectedItem is not FileEntry selected)
        {
            return;
        }
        
        Process.Start("explorer.exe", "/select, \"" + selected.FilePath + "\"");
    }
}