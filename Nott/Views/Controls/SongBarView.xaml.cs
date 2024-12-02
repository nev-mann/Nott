using Nott.ViewModels;
using System.Collections;

namespace Nott.Views.Controls;

public partial class SongBarView : ContentView
{
	public SongBarView()
	{
		InitializeComponent();
		BindingContext = MauiProgram.GetSongBarViewModel();
    }
}