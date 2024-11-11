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
    public double duration;

    [ObservableProperty]
    public double sliderValue;

    [ObservableProperty]
    public bool shuffle;
    [ObservableProperty]
    public bool repeat;

    [ObservableProperty]
    public ImageSource playSource;
    private string playImage;

    public SongBarViewModel(SoundPlayer sp)
    {
        soundPlayer = sp;
        Volume = ((int)(soundPlayer.volume * 100)).ToString() + '%';
        SliderValue = soundPlayer.volume;
        Shuffle = soundPlayer.shuffle;
        Repeat = soundPlayer.repeat;
        PlaySource = playImage = "play.png";
    }
    [RelayCommand]
    public void ValueChanged()
    {
        Volume = ((int)(SliderValue * 100)).ToString() + '%';
        soundPlayer.SetVolume(SliderValue);
    }
    [RelayCommand]
    public void PlayClicked()
    {
        if (playImage[1] == 'l')
        {
            PlaySource = playImage = "pause.png";
            soundPlayer.PauseAudio();
        }
        else
        {
            PlaySource = playImage = "play.png";
            soundPlayer.PlayAudio();
        }

    }
    [RelayCommand]
    public void RepeatClicked()
    {
        Repeat ^= true;
        soundPlayer.repeat = Repeat;
    }
    [RelayCommand]
    public void ShuffleClicked()
    {
        Shuffle ^= true;
        soundPlayer.shuffle = Shuffle;        
    }
}