using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class ResetPasswordView : ContentPage
{
	public ResetPasswordView(ResetPasswordViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}