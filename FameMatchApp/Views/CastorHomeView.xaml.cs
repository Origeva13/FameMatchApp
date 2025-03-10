using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class CastorHomeView : ContentPage
{
	public CastorHomeView(CastorHomeViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}