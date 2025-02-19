using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FameMatchApp.Services;
using FameMatchApp.Models;
using FameMatchApp.Views;
//using UIKit;

namespace FameMatchApp.ViewModels
{

    public class ResetPasswordViewModel : ViewModelBase
    {

        private FameMatchWebAPIProxy proxy;
        private IServiceProvider serviceProvider;


        public ResetPasswordViewModel(FameMatchWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;

            ChangePasswordCommand = new Command(OnChangePassword);
        }
        private int num1;
        public int Num1
        {
            get => num1;
            set
            {
                if (num1 != value)
                {
                    num1 = value;
                    //InItFieldsDataAsync();
                    OnPropertyChanged(nameof(Num1));
                }
            }
        }

        private int num2;
        public int Num2
        {
            get => num2;
            set
            {
                if (num2 != value)
                {
                    num2 = value;
                    //InItFieldsDataAsync();
                    OnPropertyChanged(nameof(Num2));
                }
            }
        }

        private int num3;
        public int Num3
        {
            get => num3;
            set
            {
                if (num3 != value)
                {
                    num3 = value;
                    //InItFieldsDataAsync();
                    OnPropertyChanged(nameof(Num3));
                }
            }
        }

        private int num4;
        public int Num4
        {
            get => num4;
            set
            {
                if (num4 != value)
                {
                    num4 = value;
                    //InItFieldsDataAsync();
                    OnPropertyChanged(nameof(Num4));
                }
            }
        }

        private string userEmail;
        public string UserEmail
        {
            get => userEmail;
            set
            {
                if (userEmail != value)
                {
                    userEmail = value;
                    //InItFieldsDataAsync();
                    OnPropertyChanged(nameof(UserEmail));
                }
            }
        }

        private string newPass;
        public string NewPass
        {
            get => newPass;
            set
            {
                newPass = value;
                OnPropertyChanged(nameof(NewPass));
            }

        }

        #region Code Numbers
        private string number1;
        public string Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged("Number1");
            }
        }

        private string number2;
        public string Number2
        {
            get => number2;
            set
            {
                number2 = value;
                OnPropertyChanged("Number2");
            }
        }

        private string number3;
        public string Number3
        {
            get => number3;
            set
            {
                number3 = value;
                OnPropertyChanged("Number3");
            }
        }

        private string number4;
        public string Number4
        {
            get => number4;
            set
            {
                number4 = value;
                OnPropertyChanged("Number4");
            }
        }

        #endregion

        public Command ChangePasswordCommand { get; }

        public async void OnChangePassword()
        {

            if (Num1.ToString() == Number1 && Num2.ToString() == Number2 && Num3.ToString() == Number3 && Num4.ToString() == Number4)
            {
                User u = await proxy.GetUserByEmail(UserEmail);
                u.UserPassword = NewPass;
                InServerCall = true;
                bool success = await proxy.UpdateUserPassword(u);

                InServerCall = false;
                if (success == true)
                {
                    await Application.Current.MainPage.DisplayAlert("Change Password", "Password has been changed", "ok");
                    ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<LogInView>());

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Change Password", "Was not able to change password", "ok");

                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Change Password", "Code is incorrect", "ok");

            }




        }


    }
}
