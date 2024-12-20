using Nott.ViewModels;

namespace Nott.Views;

public partial class SongsPage : ContentPage
{
	private SongsViewModel viewModel;
	public SongsPage(SongsViewModel vm)
	{
		InitializeComponent();
        viewModel = vm;
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        viewModel.UpdateListSongs();
        base.OnAppearing();
    }
}