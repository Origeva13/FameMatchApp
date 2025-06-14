﻿using FameMatchApp.Services;
using System.Windows.Input;
using FameMatchApp.Models;
using System.Collections.ObjectModel;


namespace FameMatchApp.ViewModels;

public class RegisterViewModel : ViewModelBase
{

    private readonly IServiceProvider serviceProvider;
    public Command RegisterCommand { get; }

    private FameMatchWebAPIProxy proxy;

    public RegisterViewModel(FameMatchWebAPIProxy proxy)
    {
        this.proxy = proxy;
        RegisterCommand = new Command(OnRegister);
        ShowPasswordCommand = new Command(OnShowPassword);
        UploadPhotoCommand = new Command(OnUploadPhoto);
        IsPassword = true;
        EmailError = "Email is required";
        PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
        FirstNameError = "First name should contain only letters";
        LastNameError = "Last name should contain only letters";
        AgeError = "Age should be a positive number";
        UserType = "1";
        AboutMe= "Not filled yet";
        Location = "Not filled yet";

       
        Kinds2 = (new Eyes()).Kinds2;
        Eyes = Kinds2[0];
        Kinds3 = (new Hair()).Kinds3;
        Hair = Kinds3[0];
        Kinds4 = (new Skin()).Kinds4;
        Color = Kinds4[0];
        Kinds = (new BodyStructure()).Kinds;
        Body = Kinds[0];
    }


    private bool isCastedChecked;
    private bool isCastorChecked;
    //private int kidsN;
    //private bool havePets;
    //private DateTime birthDate;


    //Defiine properties for each field in the registration form including error messages and validation logic

    #region FirstName
    private bool showFirstNameError;

    public bool ShowFirstNameError
    {
        get => showFirstNameError;
        set
        {
            showFirstNameError = value;
            OnPropertyChanged();
        }
    }

    private string firstName;

    public string FirstName
    {
        get => firstName;
        set
        {
            firstName = value;
            ValidateFirstName();
            OnPropertyChanged();
        }
    }

    private string firstNameError;
    public string FirstNameError
    {
        get => firstNameError;
        set
        {
            firstNameError = value;
            OnPropertyChanged();
        }
    }

    private void ValidateFirstName()
    {
        // áãé÷ä àí ùí äîùúîù ìà øé÷ åùäåà îåøëá ø÷ îàåúéåú
        if (!string.IsNullOrEmpty(FirstName))
            firstName = firstName.Trim();
        this.ShowFirstNameError = string.IsNullOrEmpty(FirstName) || !FirstName.All(char.IsLetter);
    }

    #endregion
    #region LastName
    private bool showLastNameError;

    public bool ShowLastNameError
    {
        get => showLastNameError;
        set
        {
            showLastNameError = value;
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
            ValidateLastName();
            OnPropertyChanged();
        }
    }

    private string lastNameError;
    public string LastNameError
    {
        get => lastNameError;
        set
        {
            lastNameError = value;
            OnPropertyChanged();
        }
    }

    private void ValidateLastName()
    {
        if (!string.IsNullOrEmpty(lastName))
            lastName = lastName.Trim();
        this.ShowLastNameError = string.IsNullOrEmpty(LastName) || !LastName.All(char.IsLetter);
    }
    #endregion
    #region Email
    private bool showEmailError;

    public bool ShowEmailError
    {
        get => showEmailError;
        set
        {
            showEmailError = value;
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
            ValidateEmail();
            OnPropertyChanged();
        }
    }

    private string emailError;

    public string EmailError
    {
        get => emailError;
        set
        {
            emailError = value;
            OnPropertyChanged();
        }
    }

    private void ValidateEmail()
    {
        this.ShowEmailError = string.IsNullOrEmpty(Email);
        if (!ShowEmailError)
        {
            //check if email is in the correct format using regular expression
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = "Email is not valid";
                ShowEmailError = true;
            }
            else
            {
                EmailError = "";
                ShowEmailError = false;
            }
        }
        else
        {
            EmailError = "Email is required";
        }
    }
    #endregion
    #region Password
    private bool showPasswordError;

    public bool ShowPasswordError
    {
        get => showPasswordError;
        set
        {
            showPasswordError = value;
            OnPropertyChanged();
        }
    }

    private string password;

    public string Password
    {
        get => password;
        set
        {
            password = value;
            ValidatePassword();
            OnPropertyChanged();
        }
    }

    private string passwordError;

    public string PasswordError
    {
        get => passwordError;
        set
        {
            passwordError = value;
            OnPropertyChanged();
        }
    }

    private void ValidatePassword()
    {
        //Password must include characters and numbers and be longer than 4 characters
        if (string.IsNullOrEmpty(password) ||
            password.Length < 4 ||
            !password.Any(char.IsDigit) ||
            !password.Any(char.IsLetter))
        {
            this.ShowPasswordError = true;
        }
        else
            this.ShowPasswordError = false;
    }

    //This property will indicate if the password entry is a password
    private bool isPassword = true;
    public bool IsPassword
    {
        get => isPassword;
        set
        {
            isPassword = value;
            OnPropertyChanged();
        }
    }
    //This command will trigger on pressing the password eye icon
    public Command ShowPasswordCommand { get; }
    //This method will be called when the password eye icon is pressed
    public void OnShowPassword()
    {
        //Toggle the password visibility
        IsPassword = !IsPassword;
    }
    #endregion
    #region Usertype

    private string userType;
    public string UserType
    {
        get
        {
            return userType;
        }
        set
        {
            userType = value;
            if (value == "1")
            {
                IsCastorChecked = true;
                IsCastedChecked = false;
            }
            else
            {
                IsCastorChecked = false;
                IsCastedChecked = true;
            }

            OnPropertyChanged();
        }
    }

    public bool IsCastedChecked
    {
        get { return isCastedChecked; }
        set
        {
            isCastedChecked = value;

            OnPropertyChanged();
        }
    }
    public bool IsCastorChecked
    {
        get { return isCastorChecked; }
        set
        {
            isCastorChecked = value;

            OnPropertyChanged();
        }
    }
    #endregion
    #region Gender
    private string gender;
    public string Gender
    {
        get
        {
            return gender;
        }
        set
        {
            gender = value;
            OnPropertyChanged("UserGender");
        }
    }
    #endregion
    #region Age
    private int age;

    public int Age
    {
        get => age;
        set
        {
            age = value;
            ValidateAge();
            OnPropertyChanged();
        }
    }
    private string ageError;
    public string AgeError
    {
        get => ageError;
        set
        {
            ageError = value;
            OnPropertyChanged();
        }
    }

    private bool showAgeError;
    public bool ShowAgeError
    {
        get => showAgeError;
        set
        {
            showAgeError = value;
            OnPropertyChanged();
        }
    }
    private void ValidateAge()
    {
        bool valid = true;
        string experienceString = age.ToString();
        if (experienceString.Contains(".") || experienceString.Contains(","))
        {
            valid = false;
        }
        else if (age < 0)
        {
            valid = false;
        }
        this.ShowAgeError = !valid;
    }
    #endregion 
    #region CompanyName
    private string companyName;

    public string CompanyName
    {
        get => companyName;
        set
        {
            companyName = value;
            //ValidatePayment();
            OnPropertyChanged();
        }
    }
    #endregion Payment
    #region NumOflicense

    private int numOfLicense;
    public int NumOfLicense
    {
        get => numOfLicense;
        set
        {
            numOfLicense = value;
            OnPropertyChanged();
        }
    }
    #endregion 
    #region Hight

    private int hight;
    public int Hight
    {
        get => hight;
        set
        {
            hight = value;
            OnPropertyChanged();
        }
    }
    
    #endregion kids
    #region eyes
    private string eyes;
    public string Eyes
    {
        get => eyes;
        set
        {
            eyes = value;
            OnPropertyChanged("Eyes");
        }
    }

    private List<string> kinds2;
    public List<string> Kinds2
    {
        get => kinds2;
        set
        {
            kinds2 = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region hair
    private string hair;
    public string Hair
    {
        get => hair;
        set
        {
            hair = value;
            OnPropertyChanged("Hair");
        }
    }

    private List<string> kinds3;
    public List<string> Kinds3
    {
        get => kinds3;
        set
        {
            kinds3 = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region Color
    private string color;
    public string Color
    {
        get => color;
        set
        {
            color = value;
            OnPropertyChanged();
        }
    }

    private List<string> kinds4;
    public List<string> Kinds4
    {
        get => kinds4;
        set
        {
            kinds4 = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region bodyStructure
    private string body;
    public string Body
    {
        get => body;
        set
        {
            body = value;
            OnPropertyChanged("Body");
        }
    }

    private List<string> kinds;
    public List<string> Kinds
    {
        get => kinds;
        set
        {
            kinds = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region Aboutme
    private string aboutMe;
    public string AboutMe
    {
        get => aboutMe;
        set
        {
            aboutMe = value;
            OnPropertyChanged("AboutMe");
        }
    }
    #endregion
    #region Location
    private string location;
    public string Location
    {
        get => location;
        set
        {
            location = value;
            OnPropertyChanged("Location");
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
            OnPropertyChanged();
        }
    }

    private string localPhotoPath;

    public string LocalPhotoPath
    {
        get => localPhotoPath;
        set
        {
            localPhotoPath = value;
            OnPropertyChanged();
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

    public async void OnRegister()
    {
        ValidateEmail();
        ValidatePassword();
        ValidateFirstName();
        ValidateLastName();
        ValidateAge();




        if (IsCastedChecked)
        {
            Casted? c = new Casted()
            {
                UserName = FirstName,
                UserLastName = LastName,
                UserEmail = Email,
                UserPassword = Password,
                UserGender = Gender,
                UserAge = Age,
                UserBody = Body,
                UserSkin = Color,
                UserEyes = Eyes,
                UserHair = Hair,
                UserHigth = Hight,
                UserLocation = Location,
                AboutMe = AboutMe

            };
            InServerCall = true;
            c = await proxy.RegisterCasted(c);
            InServerCall = false;
            if (c != null)
            {

                string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
                if (c.UserGender == "Female")
                {
                    hisHer = "her";
                    sheHe = "she";
                    himHer = "her";

                }
                if (c.UserGender == "Other")
                {
                    hisHer = "their";
                    sheHe = "they";
                    himHer = "them";
                }
                if (c != null)
                {
                    EmailData emailData = new EmailData()
                    {
                        From = "Fame Match App",
                        To = c.UserEmail,
                        Subject = "Welcome to Fame Match! Your account is now active",
                        Body = $@"Hi {c.UserName},

We’re thrilled to welcome you to Fame Match! Your account has been successfully approved, and you’re now part of our exclusive platform.

We can’t wait for you to start exploring the incredible opportunities and features that await. Whether you’re looking to connect with other creators, showcase your talent, or build your network, Fame Match is here to help you shine.

Here’s what you can do next:
1. Log in to your account.
2. Complete your profile to get noticed by others.
3. Start connecting with like-minded individuals in the community.

If you have any questions or need support, don’t hesitate to reach out to us at FameMatch@gmail.com. We're here to help!

We’re so excited to have you with us.

Best regards,  
The Fame Match Team",
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
        <h2>Welcome to Fame Match, {c.UserName}!</h2>
        <p>We’re excited to have you as a part of our growing community. Your account has been approved, and now you have access to all the amazing features of Fame Match.</p>
        <p>To get started, log in to your account, complete your profile, and begin connecting with other users in the platform. We’re here to support you every step of the way!</p>
        <p>If you need assistance or have any questions, feel free to contact our support team. We’re here to help!</p>
        <p>Best regards,<br><strong>The Fame Match Team</strong></p>
        <div class='footer'>
            <p>This is an automated email. Please do not reply.</p>
        </div>
    </div>
</body>
</html>"
                    };


                    SendEmailService emailService = new SendEmailService();
                    bool success = await emailService.Send(emailData);
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                    string errorMsg = "Registration Was Succesfull, \n you can now login";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }

            }
        }
        else
        {
            Castor? a = new Castor()
            {
                UserName = FirstName,
                UserLastName = LastName,
                UserEmail = Email,
                UserPassword = Password,
                NumOfLisence = NumOfLicense,
                UserGender = Gender,
                CompanyName = CompanyName,
                IsBlocked = true,
                IsAprooved = false

            };
            InServerCall = true;
            a = await proxy.RegisterCastor(a);
            InServerCall = false;
            if (a != null)
            {
                //if (!string.IsNullOrEmpty(LocalPhotoPath))
                //{
                //    await proxy.LoginAsync(new LoginInfo { Email = b.Email, Password = b.Password });
                //    Users? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                //    if (updatedUser == null)
                //    {
                //        InServerCall = false;
                //        await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Profile image upload failed", "ok");
                //    }
                //    else
                //        b.ProfileImagePath = updatedUser.ProfileImagePath;
                //}
                string hisHer = "his", sheHe = "he", hasLicense = "has", himHer = "him";
                if (a.UserGender == "Female")
                {
                    hisHer = "her";
                    sheHe = "she";
                    himHer = "her";

                }
                if (a.UserGender == "Other")
                {
                    hisHer = "their";
                    sheHe = "they";
                    himHer = "them";
                }
                if (a != null)
                {
                    EmailData emailData = new EmailData()
                    {
                        From = "Fame Match App",
                        To = a.UserEmail,
                        Subject = "Welcome to Fame Match! Your account is now active",
                        Body = $@"Hi {a.UserName},

We’re thrilled to welcome you to Fame Match! Your account has been successfully approved, and you’re now part of our exclusive platform.

We can’t wait for you to start exploring the incredible opportunities and features that await. Whether you’re looking to connect with other creators, showcase your talent, or build your network, Fame Match is here to help you shine.

Here’s what you can do next:
1. Log in to your account.
2. Complete your profile to get noticed by others.
3. Start connecting with like-minded individuals in the community.

If you have any questions or need support, don’t hesitate to reach out to us at FameMatch@gmail.com. We're here to help!

We’re so excited to have you with us.

Best regards,  
The Fame Match Team",
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
        <h2>Welcome to Fame Match, {a.UserName}!</h2>
        <p>We’re excited to have you as a part of our growing community. Your account has been approved, and now you have access to all the amazing features of Fame Match.</p>
        <p>To get started, log in to your account, complete your profile, and begin connecting with other users in the platform. We’re here to support you every step of the way!</p>
        <p>If you need assistance or have any questions, feel free to contact our support team. We’re here to help!</p>
        <p>Best regards,<br><strong>The Fame Match Team</strong></p>
        <div class='footer'>
            <p>This is an automated email. Please do not reply.</p>
        </div>
    </div>
</body>
</html>"
                    };


                    SendEmailService emailService = new SendEmailService();
                    bool success = await emailService.Send(emailData);
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                    string errorMsg = "Registration Was Succesfull, \n you need to wait until you \n will be approved to login";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }



        
        }
    }
}


