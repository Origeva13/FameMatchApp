using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class ForgotPasswordView : ContentPage
{
    public ForgotPasswordView(ForgotPassword vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}