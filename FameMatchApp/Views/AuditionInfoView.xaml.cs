using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class AuditionInfoView : ContentPage
{
	public AuditionInfoView(AuditionInfoViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}