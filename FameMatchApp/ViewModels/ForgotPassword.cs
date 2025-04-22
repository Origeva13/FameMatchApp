using Microsoft.Extensions.DependencyInjection;
using FameMatchApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FameMatchApp.Views;
using FameMatchApp.ViewModels;
using FameMatchApp.Services;

namespace FameMatchApp.ViewModels
{
    public class ForgotPassword : ViewModelBase
    {

        private FameMatchWebAPIProxy proxy;
        private SendEmailService sendEmailService;
        private IServiceProvider serviceProvider;

        public ForgotPassword(FameMatchWebAPIProxy proxy, SendEmailService sendEmailService, IServiceProvider service)
        {
            this.proxy = proxy;
            this.serviceProvider = service;
            this.sendEmailService = sendEmailService;
            Random random = new Random();
            this.Num1 = random.Next(1, 10);
            this.Num2 = random.Next(1, 10);
            this.Num3 = random.Next(1, 10);
            this.Num4 = random.Next(1, 10);
            this.From = "FameMatchService";
            this.Subject = "Change password for Fame Match";
            this.Body = "Please enter the numbers in the app to confirm your email address. \n" +
                $"The numbers are: {Num1}, {Num2}, {Num3}, {Num4}";

        }


        private string from;
        private string to;
        private string subject;
        private string body;
        private string statusMessage;
        //private ObservableCollection<EmailData> sentEmails; //if i want to see history

        #region properties
        public string From
        {
            get => from;
            set
            {
                //if (((App)Application.Current).OwnerIn == true)
                //    from = "Owner";
                //else
                from = value;
                //OnPropertyChanged(nameof(From));
            }
        }

        public string To
        {
            get => to;
            set
            {
                to = value;
                //OnPropertyChanged(nameof(To));
            }
        }

        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        public string Body
        {
            get => body;
            set
            {
                body = value;
                OnPropertyChanged(nameof(Body));
            }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set
            {
                statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        #endregion

        #region Code Numbers
        private int num1;
        public int Num1
        {
            get => num1;
            set
            {
                num1 = value;
                OnPropertyChanged("Num1");
            }
        }

        private int num2;
        public int Num2
        {
            get => num2;
            set
            {
                num2 = value;
                OnPropertyChanged("Num2");
            }
        }

        private int num3;
        public int Num3
        {
            get => num3;
            set
            {
                num3 = value;
                OnPropertyChanged("Num3");
            }
        }

        private int num4;
        public int Num4
        {
            get => num4;
            set
            {
                num4 = value;
                OnPropertyChanged("Num4");
            }
        }

        #endregion
        //public ObservableCollection<EmailData> SentEmails => sentEmails;

        // Commands or Actions
        private ICommand sendEmailCommand;
        public ICommand SendEmailCommand => sendEmailCommand ??= new Command(async () => await SendEmailAsync());

        public async Task SendEmailAsync()
        {
            InServerCall = true;
            if (string.IsNullOrWhiteSpace(Subject))
            {
                StatusMessage = "All fields are required to send an email.";
                return;
            }
            List<string> allEmails = await proxy.GetAllEmails();
            bool exists = false;
            foreach (string e in allEmails)
            {
                if (e == To)
                {
                    exists = true;
                }
            }
            if (exists == true)
            {
                var emailData = new EmailData
                {
                    From = From,
                    To = To,
                    Subject = Subject,
                    Body = Body



                };

                try
                {
                    bool isSent = await sendEmailService.Send(emailData);
                    if (isSent)
                    {
                        ResetPasswordView resetpasswordview = serviceProvider.GetService<ResetPasswordView>();
                        ((ResetPasswordViewModel)resetpasswordview.BindingContext).UserEmail = To;
                        ((ResetPasswordViewModel)resetpasswordview.BindingContext).Num1 = Num1;
                        ((ResetPasswordViewModel)resetpasswordview.BindingContext).Num2 = Num2;
                        ((ResetPasswordViewModel)resetpasswordview.BindingContext).Num3 = Num3;
                        ((ResetPasswordViewModel)resetpasswordview.BindingContext).Num4 = Num4;
                        //var navParam = new Dictionary<string, object>
                        //{
                        //      {"num1",Num1 }, {"num2", Num2} , {"num3", Num3}, {"num4", Num4}, {"email", To}
                        //};
                        ((App)Application.Current).MainPage.Navigation.PushAsync(resetpasswordview);
                        //await Shell.Current.GoToAsync("ResetPasswordView", navParam);

                        StatusMessage = "";
                        //sentEmails.Add(emailData);
                        From = "";
                        Subject = "";
                        Body = "";
                    }
                    else
                    {
                        StatusMessage = "Failed to send email.";
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error: {ex.Message}";
                }
                InServerCall = false;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fail", " Unfortunatelly your email was not found in the system", "OK");
            }

        }
    }
}
