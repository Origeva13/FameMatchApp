using FameMatchApp.ViewModels;
using FameMatchApp.Views;

namespace FameMatchApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            Routing.RegisterRoute("matchdetailsview", typeof(MatchDetailsView));
            Routing.RegisterRoute("UserInfo", typeof(UserInfoView));
            Routing.RegisterRoute("auditioninfoview", typeof(AuditionInfoView));
            Routing.RegisterRoute("ResetPasswordView", typeof(ResetPasswordView));

        }




    }
}
