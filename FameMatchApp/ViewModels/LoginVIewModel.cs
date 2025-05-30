﻿using FameMatchApp.Models;
using FameMatchApp.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FameMatchApp.ViewModels;
using FameMatchApp.Views;
using FameMatchApp.View;
namespace FameMatchApp.ViewModels
{
    public class LoginVIewModel:ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public LoginVIewModel(FameMatchWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ForgotPassCommand = new Command(OnForgotPass);
            email = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
        }

        private string email;
        private string password;

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }



        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            Loginfo loginInfo = new Loginfo { UserEmail = Email, UserPassword = Password };
            User? u = await this.proxy.LoginAsync(loginInfo);

            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                ErrorMsg = "Invalid email or password";
            }
            else if (u.IsBlocked==true)
            {
                ErrorMsg = "Unfortunately your'e blocked from the app\n              Try to contact our team at\n               FameMatch@gmail.com";

            }
            else if(u is Casted)
            {
                ErrorMsg = "";
                //Navigate to the main page
                AppShell shell = serviceProvider.GetService<AppShell>();
                //TasksViewModel tasksViewModel = serviceProvider.GetService<TasksViewModel>();
                //tasksViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
                ((App)Application.Current).MainPage = shell;
                Shell.Current.FlyoutIsPresented = false; //close the flyout
                Shell.Current.GoToAsync("CastedHomePage"); //Navigate to the Tasks tab page
            }
            //else if (u is Castor&& u.)
            //{
            //    ErrorMsg = "You haven't been aprooved by our team yet";
            //}
            else
            {

                ErrorMsg = "";
                //Navigate to the main page
                AppShell shell = serviceProvider.GetService<AppShell>();
                //TasksViewModel tasksViewModel = serviceProvider.GetService<TasksViewModel>();
                //tasksViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
                ((App)Application.Current).MainPage = shell;
                Shell.Current.FlyoutIsPresented = false; //close the flyout
                Shell.Current.GoToAsync("CastorHomePage"); //Navigate to the Tasks tab page
            }
        }

        private void OnRegister()
        {
            ErrorMsg = "";
            Email = "";
            Password = "";
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterView>());
        }
        public ICommand ForgotPassCommand { get; }

        private void OnForgotPass()
        {
            ErrorMsg = "";
            Email = "";
            Password = "";
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<ForgotPasswordView>());
        }
    }
}
