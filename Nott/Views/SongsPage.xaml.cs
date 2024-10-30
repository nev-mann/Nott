using Nott.ViewModels;

namespace Nott.Views;

public partial class SongsPage : ContentPage
{
	private SongsViewModel vm;
	public SongsPage(SongsViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		vm.UpdateListSongs();
    }
}