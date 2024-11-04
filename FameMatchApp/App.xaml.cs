using FameMatchApp.Views;
using FameMatchApp.Services;
using FameMatchApp.Models;

namespace FameMatchApp
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new AppShell();
        //}
        //Application level variables
        public User? LoggedInUser { get; set; }
    
        private FameMatchWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoggedInUser = null;
          
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

    }
}
