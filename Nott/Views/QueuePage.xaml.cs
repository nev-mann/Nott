using Nott.ViewModels;

namespace Nott.Views;

public partial class QueuePage : ContentPage
{
	public QueuePage(QueueViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}