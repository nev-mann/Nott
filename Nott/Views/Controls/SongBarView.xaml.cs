using Nott.ViewModels;

namespace Nott.Views.Controls;

public partial class SongBarView : ContentView
{
	public SongBarView()
	{
		InitializeComponent();
		BindingContext = MauiProgram.GetSongBarViewModel<SongBarViewModel>();
	}
}