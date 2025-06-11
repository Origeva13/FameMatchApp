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
    public class CastedHomeViewModel:ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        private Casted casted;
        public CastedHomeViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            User theUser = ((App)App.Current).LoggedInUser;
            this.casted = (Casted)theUser;
            // Initialize Commands
            UserName = theUser.UserName;
            UserLastName = theUser.UserLastName;
            UsersPhoto = theUser.Files;
            SendHelpEmail = new Command(OnSendHelpEmail);
            // Initialize Questions List
            CommonQuestions = new List<string>
            {
                "How do I update my profile?",
                "How do I reset my password?",
                "How do I change my email address?",
                "How do I delete my account?",
                "How can I contact support?",
                "How do I find auditions?",
                "How do I contact a castor for audition?",
                "What are the main fetures of the app?"
            };
            // Default selected question
            SelectedQuestion = CommonQuestions[0]; 
        }
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

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }

        #endregion

        #region UsersPhoto
        private List<Models.File> usersPhoto;
        public List<Models.File> UsersPhoto
        {
            get => this.usersPhoto;
            set
            {
                this.usersPhoto = value;
                OnPropertyChanged(nameof(UsersPhoto));
            }
        }
        #endregion

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

        #region Commands
        public Command SendHelpEmail { get; }
        public async void OnSendHelpEmail()
        {
            string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
            if (casted.UserGender == "Female")
            {
                hisHer = "her";
                sheHe = "she";
                himHer = "her";
            }
            if (casted.UserGender == "Other")
            {
                hisHer = "their";
                sheHe = "they";
                himHer = "them";
            }

            string answer = GetAnswerForSelectedQuestion(hisHer, sheHe);

            if (casted != null)
            {
                EmailData emailData = new EmailData()
                {
                    From = "FameMatchSupport",
                    To = casted.UserEmail,
                    Subject = $"Your Support Request: {SelectedQuestion}",
                    Body = $@"Hi {casted.UserName},

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
        <h2>Hello {casted.UserName},</h2>
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
                case "How do I find auditions?":
                    return "To find a audition, visit the 'Audition List' section on the app and press the button,then a list of auditions will apear, now just choose one.";
                case "How do I contact a castor for audition?":
                    return "After choosing audition at the 'Auition List', you will see the audition details, if you will look down you will be able to see a send email button, now just press it .";
                case "What are the main fetures of the app?":
                    return " Our app has a lot of cool features but the main is: matching between castors and casteds.";
                default:
                    return "If you have any other questions, please don't hesitate to contact support.";
            }
        }
        #endregion

    }
}
