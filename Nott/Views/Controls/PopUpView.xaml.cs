using CommunityToolkit.Maui.Views;
using Nott.ViewModels;

namespace Nott.Views.Controls;

public partial class PopUpView : Popup
{
    public PopUpView(PopUpViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}