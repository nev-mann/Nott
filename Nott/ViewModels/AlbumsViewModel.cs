using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;

namespace Nott.ViewModels;

public partial class AlbumsViewModel : ObservableObject
{
	[ObservableProperty]
	string text;

	private SoundPlayer soundPlayer;
	public AlbumsViewModel(SoundPlayer sp)
	{
        soundPlayer = sp;
        Text = sp.CurrentSong;
	}

	[RelayCommand]
	public void Update()
	{
        Text = soundPlayer.CurrentSong;
    }

}