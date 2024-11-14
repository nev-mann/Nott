using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;

namespace Nott.ViewModels;

public partial class SongBarViewModel : ObservableObject
{
    private SoundPlayer soundPlayer;
    private bool returnTask = false;

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

        soundPlayer.PlaybackStarted += UpdateView;
    }

    public void UpdateView()
    {
        PlaySource = playImage = "pause.png";
        Duration = 0;

        returnTask = false;
        Task.Run(UpdateDuration);

        if (soundPlayer.audioPlayer is not null)
            soundPlayer.audioPlayer.PlaybackEnded += delegate { 
                returnTask = true; 
                Duration = 0; 
                PlaySource = playImage = "play.png"; 
            };
    }
    private async Task UpdateDuration()
    {
        while (true)
        {
            if (returnTask) return;
            if (soundPlayer.audioPlayer != null && soundPlayer.audioPlayer.Duration != 0)
            {
                var x = soundPlayer.audioPlayer.Duration;
                var y = soundPlayer.audioPlayer.CurrentPosition;
                Duration = (soundPlayer.audioPlayer.CurrentPosition / soundPlayer.audioPlayer.Duration);
            }
            //}
            //catch(Exception ex) 
            //{
            //    await Task.Delay(1);
            //}
            await Task.Delay(200);
        }
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
            soundPlayer.PlayAudio();
        }
        else
        {
            PlaySource = playImage = "play.png";
            soundPlayer.PauseAudio();
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