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
                        From = $"Fame Match App",
                        To = SelectedCastor.UserEmail,
                        Subject = $"Congratulations! Youv'e been approoved",
                        Body = $@"Hi {SelectedCastor.UserName}, 
                        our team has reviewed your application and we are happy to inform you that you have been approved to join our platform.
                        Best Regards, The FameMatch Team"


                    };

                    SendEmailService emailService = new SendEmailService();
                    bool success = await emailService.Send(emailData);
                    await Application.Current.MainPage.DisplayAlert("success", $"Castor has been approved", "Ok");

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
                        From = $"Fame Match App",
                        To = SelectedCastor.UserEmail,
                        Subject = $"Unfortunatelly! You haven't been approoved",
                        Body = $@"Hi {SelectedCastor.UserName}, 
                        our team has reviewed your application and we are sorry to inform you that you have not been approved to join our platform.
                        Best Regards, The FameMatch Team
p.s if you think that a mistake have been made please contact us at : FameMatch@gmail.com"


                    };

                    SendEmailService emailService = new SendEmailService();
                    bool success = await emailService.Send(emailData);
                    await Application.Current.MainPage.DisplayAlert("success", $"Castor has been declained", "Ok");

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
