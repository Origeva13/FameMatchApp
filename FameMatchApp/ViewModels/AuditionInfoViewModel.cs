using FameMatchApp.Models;
using FameMatchApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedAudition), "selectedAudition")]

    public class AuditionInfoViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;

        private Audition selectedAudition;

        // Property to hold the selected Casted
        public Audition SelectedAudition
        {
            get => selectedAudition;
            set
            {
                selectedAudition = value;
                OnPropertyChanged();  // Notify the UI when the SelectedCasted is set
                LoadCastedDetails();  // Optionally, load any additional details or process the data
            }
        }
        private string audname1;
        public string AudName1
        {
            get => audname1;
            set
            {
                audname1 = value;
                OnPropertyChanged();
            }
        } 
        private string description1;
        public string Description1
        {
            get => description1;
            set
            {
                description1 = value;
                OnPropertyChanged();
            }
        }
        // Constructor 
        public AuditionInfoViewModel()
        {
            SendEmailCommand = new Command(OnSendEmail);
        }
        //לשאול את עופר
        //public bool IsManager => SelectedUser1?.IsManager ?? false;

        //public bool IsCasted => SelectedUser1 is Casted;

        //public bool IsCastor => SelectedUser1 is Castor;
        #region MsgTxt
        private string msgTxt;
        public string MsgTxt
        {
            get => msgTxt;
            set
            {
                msgTxt = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Send Email to Selected Casted
        //Command for selecting the Casted and send him an email.
        public Command SendEmailCommand { get; set; }
        private async void OnSendEmail()
        {
            User theUser = ((App)App.Current).LoggedInUser;
            Casted current = (Casted)theUser;

            Castor castor=await proxy.GetUserByAudition(SelectedAudition.AudId);//!!!לתקן
            string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
            if (current.UserGender == "Female")
            {
                hisHer = "her";
                sheHe = "she";
                himHer = "her";

            }
            if (current != null && SelectedAudition != null)
            {
                EmailData emailData = new EmailData()
                {
                    From = $"{current.UserName} {current.UserLastName}",
                    To = castor.UserEmail,
                    Subject = $"Request for '{SelectedAudition.AudName}' audition found for you",
                    Body = $@"Hi {castor.UserName}, {current.UserName} {current.UserLastName} is a Casted in our system that is interested in the {SelectedAudition.AudName}
audition, you can contact {himHer} at this Email: {current.UserEmail}
additionaly he added a massage for you: {MsgTxt}


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
            if (SelectedAudition != null)
            {
                AudName1 = SelectedAudition.AudName;  // Assume the Casted class has a Name property
                Description1 = SelectedAudition.Description;


                // Optionally, load any additional details or process the data

            }
        }
    }
}

