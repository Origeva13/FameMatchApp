using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class MatchDetailsView : ContentPage
{
	public MatchDetailsView(MatchDetailsViewModel vm)
	{
		BindingContext = vm;
        InitializeComponent();
	}
}