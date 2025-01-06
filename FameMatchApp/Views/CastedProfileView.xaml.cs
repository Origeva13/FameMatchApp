using FameMatchApp.ViewModels;
namespace FameMatchApp.Views;

public partial class CastedProfileView : ContentPage
{
	public CastedProfileView(CastedProfileViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}