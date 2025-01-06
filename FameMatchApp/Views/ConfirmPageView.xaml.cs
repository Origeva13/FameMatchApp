using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class ConfirmPageView : ContentPage
{
	public ConfirmPageView(ConfirmPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}