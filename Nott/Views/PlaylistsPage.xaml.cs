using Nott.ViewModels;

namespace Nott.Views;

public partial class PlaylistsPage : ContentPage
{
	private PlaylistsViewModel viewModel;
	public PlaylistsPage(PlaylistsViewModel vm)
	{
		InitializeComponent();
        viewModel = vm;
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if(viewModel.SelectedPlaylist is not null)
            viewModel.DisplayPlaylist(viewModel.SelectedPlaylist);
    }
}