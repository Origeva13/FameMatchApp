using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class AuditionLIstView : ContentPage
{
	public AuditionLIstView(AuditionListViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}