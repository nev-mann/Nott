using Nott.ViewModels;

namespace Nott.Views;

public partial class SongsPage : ContentPage
{
	public SongsPage(SongsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}