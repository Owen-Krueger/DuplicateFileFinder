using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace DuplicateFileFinder;

public partial class RequestUserControl : UserControl
{
    private readonly MainWindow mainWindow;
    
    public RequestUserControl(MainWindow mainWindow)
    {
        InitializeComponent();
        this.mainWindow = mainWindow;
    }
    
    private void OnBrowseFolder(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFolderDialog();
        if (openFileDialog.ShowDialog() != true || !Directory.Exists(openFileDialog.FolderName))
        {
            return;
        }
        
        TextFolderPath.Text = openFileDialog.FolderName;
        //mainWindow.ContentControl.Content = new ProgressUserControl(openFileDialog.FolderName);
        mainWindow.GetDuplicateFiles(openFileDialog.FolderName);
    }
}