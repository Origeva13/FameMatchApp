using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class UserInfoView : ContentPage
{
	public UserInfoView(UserInfo vm)
	{
		BindingContext=vm;
        InitializeComponent();
	}
}