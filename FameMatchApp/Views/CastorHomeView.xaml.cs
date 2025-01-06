using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class CastorHomeView : ContentPage
{
	public CastorHomeView(CastedHomeViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}