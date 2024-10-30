using Nott.ViewModels;

namespace Nott.Views;

public partial class AlbumsPage : ContentPage
{
	public AlbumsPage(AlbumsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}