using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class MatchView : ContentPage
{
	public MatchView(MatchViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}