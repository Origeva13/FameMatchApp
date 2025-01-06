using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class CastedHomeView : ContentPage
{
	public CastedHomeView(CastedHomeViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}