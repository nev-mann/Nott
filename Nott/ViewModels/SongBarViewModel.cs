using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;

namespace Nott.ViewModels;

public partial class SongBarViewModel : ObservableObject
{
    private SoundPlayer soundPlayer;

    [ObservableProperty]
    string volume;

    [ObservableProperty]
    public double sliderValue;    

    public SongBarViewModel()
	{
        soundPlayer = MauiProgram.GetSoundPlayer<SoundPlayer>();
        Volume = ((int)(soundPlayer.Volume*100)).ToString();
        SliderValue = soundPlayer.Volume;
    }
    [RelayCommand]
    public void ValueChanged()
    {
        Volume = ((int)(SliderValue * 100)).ToString();
        soundPlayer.Volume = SliderValue;
    }
}