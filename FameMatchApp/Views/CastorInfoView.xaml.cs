using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class CastorInfoView : ContentPage
{
	public CastorInfoView(CastorInfoViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}