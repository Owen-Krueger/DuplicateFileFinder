using System.Windows;

namespace DuplicateFileFinder;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ContentControl.Content = new RequestUserControl(this);
    }

    public async void GetDuplicateFiles(string directory)
    {
        var progressControl = new ProgressUserControl(directory);
        ContentControl.Content = progressControl;
        Dictionary<string, List<FileEntry>> duplicateFiles = [];
        
        await Task.Run(() =>
        {
            duplicateFiles = progressControl.GetDuplicateFiles();
        });

        progressControl.BarProgress.Visibility = Visibility.Hidden;
        progressControl.TextProgress.Text = "Duplicates found! See Results window.";
        var resultsWindow = new ResultsWindow(duplicateFiles);
        resultsWindow.Show();
        Close();
    }
}