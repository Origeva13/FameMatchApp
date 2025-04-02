using FameMatchApp.Models;
using FameMatchApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FameMatchApp.ViewModels
{
    public class CastorHomeViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        private Castor castor;
        public CastorHomeViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            User theUser = ((App)App.Current).LoggedInUser;
            this.castor = (Castor)theUser;
            ReadAuditions();
            // Initialize Commands
            IsPublicCommand = new Command<Audition>(OnPublic);
            IsntPublicCommand = new Command<Audition>(OnNotPublic);
            SendHelpEmail = new Command(OnSendHelpEmail);
            UsersAud = new ObservableCollection<Audition>();
            UserName = theUser.UserName;
            UserLastName = theUser.UserLastName;

            // Initialize Questions List
            CommonQuestions = new List<string>
            {
                "How do I update my profile?",
                "How do I reset my password?",
                "How do I change my email address?",
                "How do I delete my account?",
                "How can I contact support?",
                "How do I find matches?",
                "How do I make my auditions public / not public?",
                "How can I edit an audition ?",
                "What are the main fetures of the app?"
            };

            // Default selected question
            SelectedQuestion = CommonQuestions[0];     
            IsRefreshing = false;
            RefreshingCommand = new Command(OnRefreshing); 
        }

        #region Common Questions
        public List<string> CommonQuestions { get; }
        private string selectedQuestion;
        public string SelectedQuestion
        {
            get => selectedQuestion;
            set
            {
                selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }
        #endregion

        #region IsPublic
        private bool isPublic;
        public bool IsPublic
        {
            get => isPublic;
            set
            {
                isPublic = value;
                OnPropertyChanged("IsPublic");
            }
        }
        #endregion

        #region UsersAud
        private ObservableCollection<Audition> usersAud;
        public ObservableCollection<Audition> UsersAud
        {
            get => this.usersAud;
            set
            {
                this.usersAud = value;
                OnPropertyChanged(nameof(UsersAud));
            }
        }

        private async void ReadAuditions()
        {
            List<Audition> list = await proxy.GetUserAudition(castor.UserId);
            this.UsersAud = new ObservableCollection<Audition>(list);
            int numAud = 0;
            foreach (Audition a in UsersAud)
            {
                numAud++;
            }
            if (numAud > 0)
            {
                DoseHave = true;
            }
            else
            {
                DoseHave = false;
            }
            if (DoseHave == false)
            {
                Msg = true;
            }
            else
            {
                Msg = false;
            }
        }
        #endregion

        #region DoesHave
        private bool doseHave;
        public bool DoseHave
        {
            get => doseHave;
            set
            {
                doseHave = value;
                OnPropertyChanged("DoseHave");
            }
        }
        #endregion

        #region Msg
        private bool msg;
        public bool Msg
        {
            get => msg;
            set
            {
                msg = value;
                OnPropertyChanged("Msg");
            }
        }
        #endregion

        #region UserName
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        #endregion

        #region UserLastName
        private string userLastName;
        public string UserLastName
        {
            get => userLastName;
            set
            {
                userLastName = value;
                OnPropertyChanged("UserLastName");
            }
        }
        #endregion

        #region Commands
        public Command SendHelpEmail { get; }
        public Command IsPublicCommand { get; }
        public Command IsntPublicCommand { get; }
        public async void OnPublic(Audition aud)//להוסיף
        {
            if (aud.IsPublic == true)
            {
                await Shell.Current.DisplayAlert("Audition", $"The audition is already public!", "ok");
                return;
            }
            else
            {
                aud.IsPublic = true;
                await proxy.UpdateAudition(aud);
                InServerCall = false;
                await Shell.Current.DisplayAlert("Audition", $"The audition is now public!", "ok");
            }
        }
        public async void OnNotPublic(Audition aud)//להוסיף
        {
            if(aud.IsPublic == false)
            {
                await Shell.Current.DisplayAlert("Audition", $"The audition is already not public!", "ok");
                return;
            }
            else
            {
                aud.IsPublic = false;
                await proxy.UpdateAudition(aud);
                InServerCall = false;
                await Shell.Current.DisplayAlert("Audition", $"The audition is now not public!", "ok");
            }
           
        }
        public async void OnSendHelpEmail()
        {
            string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
            if (castor.UserGender == "Female")
            {
                hisHer = "her";
                sheHe = "she";
                himHer = "her";
            }
            if (castor.UserGender == "Other")
            {
                hisHer = "their";
                sheHe = "they";
                himHer = "them";
            }

            string answer = GetAnswerForSelectedQuestion(hisHer, sheHe);

            if (castor != null)
            {
                EmailData emailData = new EmailData()
                {
                    From = "FameMatchSupport@gmail",
                    To = castor.UserEmail,
                    Subject = $"Your Support Request: {SelectedQuestion}",
                    Body = $@"Hi {castor.UserName},

Thank you for reaching out to Fame Match! You asked: 

**{SelectedQuestion}**

Here is your answer:

{answer}

If you need further assistance, feel free to reply to this email or contact us directly at **FameMatchSupport@gmail.com**.

Best regards,  
The Fame Match Support Team",
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
        <h2>Hello {castor.UserName},</h2>
        <p>Thank you for reaching out to Fame Match! You asked:</p>
        <h3>{SelectedQuestion}</h3>
        <p><strong>Answer:</strong><br>{answer}</p>
        <p>If you need further assistance, feel free to reply to this email or contact us directly at <strong>FameMatchSupport@gmail.com</strong>.</p>
        <p>Best regards,<br><strong>The Fame Match Support Team</strong></p>
        <div class='footer'>
            <p>This is an automated email. Please do not reply directly to this message.</p>
        </div>
    </div>
</body>
</html>"
                };

                SendEmailService emailService = new SendEmailService();
                bool success = await emailService.Send(emailData);
                await Application.Current.MainPage.DisplayAlert("Success", "Support email has been sent to your email", "Ok");
            }
        }

        // Method to get the answer based on the selected question
        private string GetAnswerForSelectedQuestion(string hisHer, string sheHe)
        {
            switch (SelectedQuestion)
            {
                case "How do I update my profile?":
                    return $@"To update {hisHer} profile, log in to your account and click on {hisHer} profile icon. Then select 'Edit Profile' to make changes.";
                case "How do I reset my password?":
                    return "To reset your password, go to the login screen and click on 'Forgot Password'. Follow the instructions in your email to reset it.";
                case "How do I change my email address?":
                    return "To change your email address, log in to your account and click on the profile icon. Then select 'Edit Email', and update your email.";
                case "How do I delete my account?":
                    return "To delete your account, please contact our support team at FameMatchSupport@gmail.com, and we will assist you with the deletion process.";
                case "How can I contact support?":
                    return "You can contact support by replying to this email or by reaching us at FameMatchSupport@gmail.com.";
                case "How do I find matches?":
                    return "To find a match, visit the 'Match' section on the app and select your criterias and press the MATCH button.";
                case "How do I make my auditions public / not public?":
                    return "To view your audition , go to the 'Home Page' section and swipe right to make the audition public and left to make it not public  .";
                case "How can I edit an audition ?":
                    return "Unfortunatelly you can't edit an audition but you can make it unviseble by make it not public in your 'Home Page'.";
                case "What are the main fetures of the app?":
                    return " Our app has a lot of cool features but the main is: matching between castors and casteds.";
                default:
                    return "If you have any other questions, please don't hesitate to contact support.";
            }
        }
        #endregion

        #region Refreshing
        private bool isRefreshing;
        public bool IsRefreshing
        {

            get => this.isRefreshing;
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshingCommand { get; }

        public async void OnRefreshing()
        {
            ReadAuditions();
            IsRefreshing = false;
        }

    }
    #endregion

}

