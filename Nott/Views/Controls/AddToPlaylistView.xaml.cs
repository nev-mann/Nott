using CommunityToolkit.Maui.Views;
using Nott.ViewModels;

namespace Nott.Views.Controls;

public partial class AddToPlaylistView : Popup
{
    public AddToPlaylistView(AddToPlaylistViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.ClosePopup += ClosePopup;
    }

    private void ClosePopup()
    {
        Close();
    }
}