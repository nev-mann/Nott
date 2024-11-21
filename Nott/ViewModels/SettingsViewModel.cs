using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private AppSettings appSettings;

    [ObservableProperty]
    bool shuffle;

    [ObservableProperty]
    ObservableCollection<string> songsFolders;

    public SettingsViewModel(AppSettings s)
    {
        appSettings = s;
        SongsFolders = new ObservableCollection<string>(appSettings.settings.SongsFolders);
        shuffle = appSettings.settings.Shuffle;
    }

    [RelayCommand]
    public void Saving()
    {
        appSettings.settings.Shuffle = Shuffle;
        appSettings.Save();
    }

    [RelayCommand]
    public async Task AddFolder(string folderPath)
    {
        try
        {
            var result = await FolderPicker.Default.PickAsync();
            if (result.Folder is not null)
            {
                appSettings.settings.SongsFolders.Add(result.Folder.Path);
                SongsFolders.Add(result.Folder.Path);
            }
        }
        catch
        {
            // The user canceled or something went wrong
        }
    }

    [RelayCommand]
    public void RemoveFolder(string folderPath)
    {
        appSettings.settings.SongsFolders.Remove(folderPath);
        SongsFolders.Remove(folderPath);
    }
}