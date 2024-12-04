using FameMatchApp.Models;
using FameMatchApp.View;
using FameMatchApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.ViewModels
{
    internal class AppShellViewModel:ViewModelBase
    {
        private User? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }

        public bool IsManger
        {
            get
            {
                return this.currentUser.IsManager;
            }
        }
        public bool IsCasted
        {
            get
            {
                if (this.currentUser is Casted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsCastor
        {
            get
            {
                if (this.currentUser is Castor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LogInView>());
        }
    }
}

