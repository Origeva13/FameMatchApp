﻿using FameMatchApp.Models;
using FameMatchApp.View;
using FameMatchApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FameMatchApp.ViewModels
{
    public class AppShellViewModel:ViewModelBase
    {
        private User? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }
        public bool IsManager => currentUser?.IsManager ?? false;

        public bool IsCasted => currentUser is Casted;

        public bool IsCastor => currentUser is Castor;
        //this command will be used for logout menu item
        public ICommand LogoutCommand
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

