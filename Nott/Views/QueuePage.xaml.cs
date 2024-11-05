using Nott.ViewModels;

namespace Nott.Views;

public partial class QueuePage : ContentPage
{
    QueueViewModel viewModel;
	public QueuePage(QueueViewModel vm)
	{
		InitializeComponent();
        viewModel = vm;
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        viewModel.UpdateView();
        base.OnAppearing();
    }
}