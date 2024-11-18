using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;
using System.Collections.ObjectModel;

namespace Nott.ViewModels;

public partial class PlaylistsViewModel : ObservableObject
{
	private readonly SoundPlayer soundPlayer;
	private readonly AppSettings appSettings;

    [ObservableProperty]
    ObservableCollection<List<Song>> listOfPlaylist;

    public PlaylistsViewModel(SoundPlayer sp, AppSettings settings)
    {
        soundPlayer = sp;
        appSettings = settings;
        //listOfPlaylist = new ObservableCollection<Song>(appSettings.);
    }

    [RelayCommand]
    public void Func()
    {    
    }    
}