using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FameMatchApp.Models;
using FameMatchApp.Services;

namespace FameMatchApp.ViewModels
{
    public class CastedProfileViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        public CastedProfileViewModel(FameMatchWebAPIProxy proxy)
        {
            User theUser = ((App)App.Current).LoggedInUser;
            Casted casted = (Casted)theUser;
            this.proxy = proxy;
            Name = casted.UserName;
            LastName = casted.UserLastName;
            Email = casted.UserEmail;
            Password = casted.UserPassword;
            UserGender = casted.UserGender;
            Kinds3 = (new Hair()).Kinds3;
            Hair = Kinds3[0];
            Hight =casted.UserHigth;
            AboutMe = casted.AboutMe;
            Kinds2 = (new Eyes()).Kinds2;
            Eyes = Kinds2[0];
            //BodyStructure = casted.UserBody;
            Kinds = (new BodyStructure()).Kinds;
            BodyStructure = casted.UserBody;
            //BodyStructure = Kinds[0];
            Kinds4 = (new Skin()).Kinds4;
            Skin = Kinds4[0];
            Kinds1 = (new Age()).Kinds1;
            Age = casted.UserAge;
            PhotoURL = casted.UserImageUrl;
            //////UpdatePhotoURL(casted.ProfileImagePath);
            //////LocalPhotoPath = "";
            IsPassword = true;
            SaveCommand = new Command(OnSave);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            AboutMeError = "About me filed must be at list 20 charctares long ";
        }

        //Defiine properties for each field in the registration form including error messages and validation logic
        #region Name
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
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
                OnPropertyChanged("ShowLastNameError");
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
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
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
                OnPropertyChanged("ShowEmailError");
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
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
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
                OnPropertyChanged("ShowPasswordError");
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
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
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
                OnPropertyChanged("IsPassword");
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
        #region UserGender
        //private bool showUserGenderError;

        //public bool ShowUserGenderError
        //{
        //    get => showUserGenderError;
        //    set
        //    {
        //        showUserGenderError = value;
        //        OnPropertyChanged("ShowUserGenderError");
        //    }
        //}

        private string userGender;

        public string UserGender
        {
            get => userGender;
            set
            {
                userGender = value;
                //ValidateUserGender();
                OnPropertyChanged("UserGender");
            }
        }

        //private string userGenderError;

        //public string UserGenderError
        //{
        //    get => userGenderError;
        //    set
        //    {
        //        userGenderError = value;
        //        OnPropertyChanged("UserGenderError");
        //    }
        //}

        //private void ValidateUserGender()
        //{
        //    this.ShowUserGenderError = string.IsNullOrEmpty(UserGender);
        //}
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
        #region hight
        private int hight;

        public int Hight
        {
            get => hight;
            set
            {
                hight = value;
                OnPropertyChanged("Hight");
            }
        }
        #endregion
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
        #region bodyStructure
        private string bodyStructure;
        public string BodyStructure
        {
            get => bodyStructure;
            set
            {
                bodyStructure = value;
                OnPropertyChanged("BodyStructure");
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
        #region skin
        private string skin;
        public string Skin
        {
            get => skin;
            set
            {
                skin = value;
                OnPropertyChanged("Skin");
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
        #region AboutMe
        private bool showAboutMeError;

        public bool ShowAboutMeError
        {
            get => showAboutMeError;
            set
            {
                showAboutMeError = value;
                OnPropertyChanged("ShowAboutMeError");
            }
        }

        private string aboutMe;

        public string AboutMe
        {
            get => aboutMe;
            set
            {
                aboutMe = value;
                ValidateAboutMe();
                OnPropertyChanged("AboutMe");
            }
        }

        private string aboutMeError;

        public string AboutMeError
        {
            get => aboutMeError;
            set
            {
                aboutMeError = value;
                OnPropertyChanged("AboutMeError");
            }
        }

        private void ValidateAboutMe()
        {
            if (this.AboutMe.Length < 20)
            {
                this.ShowAboutMeError = string.IsNullOrEmpty(AboutMe);
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
        #region age
        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private List<int> kinds1;
        public List<int> Kinds1
        {
            get => kinds1;
            set
            {
                kinds1 = value;
                OnPropertyChanged();
            }
        }
        #endregion






        //Define a command for the Save button
        public Command SaveCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnSave()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidateAboutMe();

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowAboutMeError)
            {
                //Update AppUser object with the data from the Edit form
                User theUser = ((App)App.Current).LoggedInUser;
                Casted casted = (Casted)theUser;
                casted.UserName = Name;
                casted.UserLastName = LastName;
                casted.UserEmail = Email;
                casted.UserPassword = Password;
                casted.UserGender = UserGender;
                casted.UserHigth = Hight;
                casted.UserHair = Hair;
                casted.UserEyes= Eyes;
                casted.UserBody=BodyStructure;
                casted.UserSkin = Skin;
                casted.AboutMe = AboutMe;
                casted.UserAge = Age;

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdateCasted(casted);


                //If the save was successful, navigate to the login page
                if (success)
                {
                    //UPload profile image if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        User? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            await Shell.Current.DisplayAlert("Save Profile", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }


                    }
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "Save Profile failed. Please try again.";
                    await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
                }
            }
        }
    }
}
