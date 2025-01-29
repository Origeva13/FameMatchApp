using FameMatchApp.ViewModels;

namespace FameMatchApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
     
       


    }
}
