using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class AddAuditionView : ContentPage
{
	public AddAuditionView(AddAuditionViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}