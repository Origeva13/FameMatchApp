using FameMatchApp.ViewModels;

namespace FameMatchApp.Views;

public partial class UsersListView : ContentPage
{
	public UsersListView(UsersListViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}