using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class LogInView : ContentPage
{
    public LogInView(LoginVIewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}