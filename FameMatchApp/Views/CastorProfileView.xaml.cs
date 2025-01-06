using FameMatchApp.ViewModels;
namespace FameMatchApp.Views;

public partial class CastorProfileView : ContentPage
{
	public CastorProfileView(CastorProfileViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}