using FameMatchApp.Models;
using FameMatchApp.Services;
using System;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedCastor), "selectedCastor")]

    public class CastorInfoViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;

        private Castor selectedCastor;

        // Property to hold the selected Castor
        public Castor SelectedCastor
        {
            get => selectedCastor;
            set
            {
                selectedCastor = value;
                OnPropertyChanged();  // Notify the UI when the SelectedCasted is set
                LoadCastedDetails();  // Optionally, load any additional details or process the data
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        private string companyName;
        public string CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChanged();
            }
        }
        private int numOFId;
        public int NumOFId
        {
            get => numOFId;
            set
            {
                numOFId = value;
                OnPropertyChanged();
            }
        }


        // Constructor 
        public CastorInfoViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy=proxy;
            AcceptCommand = new Command(OnAccept);
            DeclaineCommand = new Command(OnDeclaine);
        }
        public Command AcceptCommand { get; }

        public async void OnAccept()
        {
            selectedCastor.IsAprooved = true;
            selectedCastor.IsBlocked = false;
            bool isWorking = await proxy.Accept(selectedCastor);
            if (isWorking == true)
            {

                string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
                if (SelectedCastor.UserGender == "Female")
                {
                    hisHer = "her";
                    sheHe = "she";
                    himHer = "her";

                }
                if (SelectedCastor.UserGender == "Other")
                {
                    hisHer = "their";
                    sheHe = "they";
                    himHer = "them";
                }
                if (SelectedCastor != null)
                {
                    EmailData emailData = new EmailData()
                    {
                        From = "Fame Match App",
                        To = SelectedCastor.UserEmail,
                        Subject = "Congratulations! You've been approved",
                        Body = $@"Hi {SelectedCastor.UserName}, 
    our team has reviewed your application and we are happy to inform you that you have been approved to join our platform.
    Best Regards, The FameMatch Team",
                        HtmlBody = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    color: #333;
                    background-color: #f8f8f8;
                    padding: 20px;
                }}
                .container {{
                    background: white;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    max-width: 600px;
                    margin: auto;
                }}
                h2 {{
                    color: #1E90FF;
                }}
                p {{
                    font-size: 16px;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 14px;
                    color: #777;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Congratulations, {SelectedCastor.UserName}!</h2>
                <p>Our team has reviewed your application, and we are happy to inform you that you have been approved to join our platform.</p>
                <p>We are excited to have you onboard and look forward to your contributions!</p>
                <p>Best Regards,<br><strong>The FameMatch Team</strong></p>
                <div class='footer'>
                    <p>This is an automated email. Please do not reply.</p>
                </div>
            </div>
        </body>
        </html>"
                    };

                    SendEmailService emailService = new SendEmailService();
                    bool success = await emailService.Send(emailData);
                    await Application.Current.MainPage.DisplayAlert("Success", "Castor has been approved", "Ok");


                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Castor hasn't been approved", "Ok");

                }
            }
        }
        public Command DeclaineCommand { get; }

        public async void OnDeclaine()
        {
            selectedCastor.IsAprooved = false;  
            selectedCastor.IsBlocked = true;
            bool isWorking = await proxy.Declaine(selectedCastor);
            if (isWorking == true)
            {

                string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
                if (SelectedCastor.UserGender == "Female")
                {
                    hisHer = "her";
                    sheHe = "she";
                    himHer = "her";

                }
                if (SelectedCastor.UserGender == "Other")
                {
                    hisHer = "their";
                    sheHe = "they";
                    himHer = "them";
                }
                if (SelectedCastor != null)
                {
                    EmailData emailData = new EmailData()
                    {
                        From = "Fame Match App",
                        To = SelectedCastor.UserEmail,
                        Subject = "Unfortunately, You Haven't Been Approved",
                        Body = $@"Hi {SelectedCastor.UserName}, 
    our team has reviewed your application, and we are sorry to inform you that you have not been approved to join our platform.
    
    Best Regards, 
    The FameMatch Team
    
    P.S. If you believe a mistake has been made, please contact us at: FameMatch@gmail.com",
                        HtmlBody = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    color: #333;
                    background-color: #f8f8f8;
                    padding: 20px;
                }}
                .container {{
                    background: white;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    max-width: 600px;
                    margin: auto;
                    text-align: center;
                }}
                h2 {{
                    color: #FF4C4C;
                }}
                p {{
                    font-size: 16px;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 14px;
                    color: #777;
                }}
                .contact {{
                    margin-top: 10px;
                    font-weight: bold;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Unfortunately, You Haven't Been Approved</h2>
                <p>Hi {SelectedCastor.UserName},</p>
                <p>Our team has reviewed your application, and we regret to inform you that you have not been approved to join our platform.</p>
                <p>We appreciate your interest in FameMatch and encourage you to apply again in the future.</p>
                <p class='contact'>P.S. If you believe a mistake has been made, please contact us at: <a href='mailto:FameMatch@gmail.com'>FameMatch@gmail.com</a></p>
                <p>Best Regards,<br><strong>The FameMatch Team</strong></p>
                <div class='footer'>
                    <p>This is an automated email. Please do not reply.</p>
                </div>
            </div>
        </body>
        </html>"
                    };

                    SendEmailService emailService = new SendEmailService();
                    bool success = await emailService.Send(emailData);
                    await Application.Current.MainPage.DisplayAlert("Success", "Castor has been declined", "Ok");


                }


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Castor hasn't been declained", "Ok");

            }
        }
        private void LoadCastedDetails()
        {
            if (SelectedCastor != null)
            {
                Name = SelectedCastor.UserName;  // Assume the Casted class has a Name property
                LastName = SelectedCastor.UserLastName;        // Assume Casted has a Bio property
                Email = SelectedCastor.UserEmail;
                CompanyName = SelectedCastor.CompanyName;
                NumOFId = SelectedCastor.NumOfLisence;
            }
        }
    }
}
