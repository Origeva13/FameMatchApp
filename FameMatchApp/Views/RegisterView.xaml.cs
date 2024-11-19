using FameMatchApp.ViewModels;
namespace FameMatchApp.View;

public partial class RegisterView : ContentPage
{
    public RegisterView(RegisterViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }

}