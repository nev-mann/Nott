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
	string folderPath;

    [ObservableProperty]
    ObservableCollection<string> songsFolders;

    [RelayCommand]
	public void Saving()
	{
        appSettings.settings.Shuffle = Shuffle;
        appSettings.Save();
	}

	[RelayCommand]
	public void AddFolder(string folderPath)
	{
		appSettings.settings.SongsFolders.Add(folderPath);
		SongsFolders.Add(folderPath);
	}

	[RelayCommand]
	public void RemoveFolder(string folderPath)
	{
		appSettings.settings.SongsFolders.Remove(folderPath);
		SongsFolders.Remove(folderPath);
	}

	public SettingsViewModel(AppSettings s)
    {
        appSettings = s;
        SongsFolders = new ObservableCollection<string>(appSettings.settings.SongsFolders);
        shuffle = appSettings.settings.Shuffle;
	}
}