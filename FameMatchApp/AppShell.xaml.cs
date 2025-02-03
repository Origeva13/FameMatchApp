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
        }
     
       


    }
}
