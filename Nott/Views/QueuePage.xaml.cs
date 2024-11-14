using Nott.ViewModels;

namespace Nott.Views;

public partial class QueuePage : ContentPage
{
    private QueueViewModel viewmodel;
    public QueuePage(QueueViewModel vm)
	{
		InitializeComponent();
        viewmodel = vm;
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        viewmodel.UpdateView();
        base.OnAppearing();
    }
}