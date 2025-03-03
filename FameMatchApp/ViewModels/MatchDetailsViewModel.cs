using FameMatchApp.Models;
using FameMatchApp.Services;
using Microsoft.Maui.Graphics;
using System;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedCasted), "selectedCasted")]
    public class MatchDetailsViewModel : ViewModelBase
    {
        private Casted selectedCasted;

        // Property to hold the selected Casted
        public Casted SelectedCasted
        {
            get => selectedCasted;
            set
            {
                selectedCasted = value;
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
        private string aboutMe;
        public string AboutMe
        {
            get => aboutMe;
            set
            {
                aboutMe = value;
                OnPropertyChanged();
            }
        }
        // Constructor 
        public MatchDetailsViewModel()
        {
            SendEmailCommand = new Command(OnSendEmail);//לתקן
        }
        #region Send Email to Selected Casted
        //Command for selecting the Casted and send him an email.
        public Command SendEmailCommand { get; set; }
        private async void OnSendEmail(Casted c)
        {
            User theUser = ((App)App.Current).LoggedInUser;
            Castor current = (Castor)theUser;
            string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
            if (current.UserGender == "Female")
            {
                hisHer = "her";
                sheHe = "she";
                himHer = "her";

            }
            if (current != null && c != null)
            {
                EmailData emailData = new EmailData()
                {
                    From = $"{current.UserName} {current.UserLastName}",
                    To = c.UserEmail,
                    Subject = $"A matching audition found for you",
                    Body = $@"Hi {c.UserName}, {current.UserName} {current.UserLastName} is a Castor in our system that is interested to invite you
to an audition, you can contact {himHer} at this Email: {current.UserEmail}


Best Regards, The FameMatch Team and {current.UserName}"

                };

                SendEmailService emailService = new SendEmailService();
                bool success = await emailService.Send(emailData);
                if (success)
                {
                    await Application.Current.MainPage.DisplayAlert("Email", "Email Was Sent Successully", "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Email", "Email Was NOT Sent, try again later", "Ok");
            }

        }
        #endregion
        private void LoadCastedDetails()
        {
            if (SelectedCasted != null)
            {
                Name = SelectedCasted.UserName;  // Assume the Casted class has a Name property
                LastName = SelectedCasted.UserLastName;        // Assume Casted has a Bio property
                Email = SelectedCasted.UserEmail;
                AboutMe = selectedCasted.AboutMe;// Assume a URL for the casted's photo
            }
        }
    }
}
