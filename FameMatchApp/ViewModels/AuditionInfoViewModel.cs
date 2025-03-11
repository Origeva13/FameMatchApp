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
        public AuditionInfoViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            SendEmailCommand = new Command(OnSendEmail);
        }

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

            Castor castor=await proxy.GetUserByAudition(SelectedAudition.AudId);
            string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
            if (current.UserGender == "Female"|| current.UserGender == "female")
            {
                hisHer = "her";
                sheHe = "she";
                himHer = "her";

            }
            if (current.UserGender == "Other")
            {
                hisHer = "their";
                sheHe = "they";
                himHer = "them";
            }
            if (current != null && SelectedAudition != null)
            {
                if (current != null && SelectedAudition != null)
                {
                    EmailData emailData = new EmailData()
                    {
                        From = $"{current.UserName} {current.UserLastName}",
                        To = castor.UserEmail,
                        Subject = $"Request for '{SelectedAudition.AudName}' audition found for you",
                        Body = $@"Hi {castor.UserName}, {current.UserName} {current.UserLastName} is a Casted in our system that is interested in the {SelectedAudition.AudName} audition, you can contact {himHer} at this Email: {current.UserEmail}. Additionally, {sheHe} added a message for you: {MsgTxt}

Best Regards, 
The FameMatch Team and {current.UserName} {current.UserLastName}",
                        HtmlBody = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            width: 100%;
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            text-align: center;
            padding-bottom: 20px;
        }}
        .header img {{
            width: 150px;
            height: auto;
        }}
        .content {{
            font-size: 16px;
            color: #333;
            line-height: 1.6;
        }}
        .content p {{
            margin-bottom: 20px;
        }}
        .footer {{
            text-align: center;
            padding-top: 20px;
            font-size: 14px;
            color: #888;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <img src='cid:file:///C:/Users/User/Downloads/DALL%C2%B7E%202025-03-11%2015.00.34%20-%20A%20modern,%20sleek%20logo%20for%20'FameMatch',%20an%20app%20connecting%20actors%20and%20casting%20professionals.%20The%20design%20features%20a%20golden%20star%20intertwined%20with%20two%20shaki.webp' alt='FameMatch Logo'/>
        </div>
        <div class='content'>
            <p>Hi {castor.UserName},</p>
            <p>{current.UserName} {current.UserLastName} is a Casted in our system that is interested in the <strong>{SelectedAudition.AudName}</strong> audition. You can contact {himHer} at this email: <strong>{current.UserEmail}</strong>.</p>
            <p>Additionally, {sheHe} added a message for you:</p>
            <blockquote>{MsgTxt}</blockquote>
<p>Here is some information about the candidate:</p>
<blockquote>{current.AboutMe}</blockquote>
            <p>Best Regards,<br>The FameMatch Team and {current.UserName} {current.UserLastName}</p>
        </div>
        <div class='footer'>
            <p>&copy; 2025 FameMatch. All rights reserved.</p>
        </div>
    </div>
</body>
</html>"
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

        }
        #endregion
        private void LoadCastedDetails()
        {
            if (SelectedAudition != null)
            {
                AudName1 = SelectedAudition.AudName; 
                Description1 = SelectedAudition.Description;



            }
        }
    }
}

