using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FameMatchApp.Services;
using FameMatchApp.Models;

namespace FameMatchApp.ViewModels
{
    internal class RegisterViewModel:ViewModelBase
    {
            
        private FameMatchWebAPIProxy proxy;
        public RegisterViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            IsPassword = true;
            FirstNameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            UserGenderError = "Please choose Gender";
            CompanyNameError = "Company Name is required";
            NumOfLicenseError = "Number of license is required";
            AgeError = "The Age Filed should be renter due to several reasons";
            LocationError = "Location is requierd";
        }

        //Defiine properties for each field in the registration form including error messages and validation logic
        #region  FirstName
        private bool showFirstNameError;

        public bool ShowFirstNameError
        {
            get => showFirstNameError;
            set
            {
                showFirstNameError = value;
                OnPropertyChanged("ShowFirstNameError");
            }
        }

        private string Firstname;

        public string FirstName
        {
            get => Firstname;
            set
            {
                Firstname = value;
                ValidateName();
                OnPropertyChanged("FirstName");
            }
        }

        private string FirstnameError;

        public string FirstNameError
        {
            get => FirstnameError;
            set
            {
                FirstnameError = value;
                OnPropertyChanged("FirstNameError");
            }
        }

        private void ValidateName()
        {
            this.ShowFirstNameError = string.IsNullOrEmpty(FirstName);
        }
        #endregion
        #region UserGender
       
        private bool showUserGenderError;

        public bool ShowUserGenderError
        {
            get => showUserGenderError;
            set
            {
                showUserGenderError = value;
                OnPropertyChanged("ShowUserGenderError");
            }
        }

        private string userGender;
        public string UserGender
        {
            get => UserGender;
            set
            {
                UserGender = value;
                ValidateName();
                OnPropertyChanged("UserGender");
            }
        }

        private string userGenderError;

        public string UserGenderError
        {
            get => UserGenderError;
            set
            {
                UserGenderError = value;
                OnPropertyChanged("UserGenderError");
            }
        }

        private void ValidateUserGender()
        {
            this.ShowUserGenderError = string.IsNullOrEmpty(UserGender);
        }
        #endregion
        #region CompanyName
        private bool showcompanyNameError;

        public bool ShowCompanyNameError
        {
            get => ShowCompanyNameError;
            set
            {
                ShowCompanyNameError = value;
                OnPropertyChanged("CompanyNameError");
            }
        }

        private string Companyname;

        public string CompanyName
        {
            get => Companyname;
            set
            {
                Companyname = value;
                ValidateCompanyName();
                OnPropertyChanged("CompanyName");
            }
        }

        private string CompanynameError;

        public string CompanyNameError
        {
            get => CompanynameError;
            set
            {
               CompanynameError = value;
                OnPropertyChanged("CompanyNameError");
            }
        }

        private void ValidateCompanyName()
        {
            this.ShowCompanyNameError = string.IsNullOrEmpty(Companyname);
        }
        #endregion
        #region NumOfLicense
        private bool shownumoflicenseError;

        public bool ShowNumofLicenseError
        {
            get => ShowNumofLicenseError;
            set
            {
                ShowNumofLicenseError = value;
                OnPropertyChanged("NumOfLicenseError");
            }
        }

        private int numOfLicense;

        public int NumOfLicense
        {
            get => NumOfLicense;
            set
            {
                NumOfLicense = value;
                ValidateNumOfLicense();
                OnPropertyChanged("NumOfLicense");
            }
        }

        private string numOfLicenseError;

        public string NumOfLicenseError
        {
            get => NumOfLicenseError;
            set
            {
                NumOfLicenseError = value;
                OnPropertyChanged("NumOfLicenseError");
            }
        }

        private void ValidateNumOfLicense()
        {
                // Perform validation checks
                if (NumOfLicense <= 0)
                {
                this.ShowNumofLicenseError = true;           
                }
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
        #region Age
        private bool showAgeError;

        public bool ShowAgeError
        {
            get => ShowAgeError;
            set
            {
                ShowAgeError = value;
                OnPropertyChanged("AgeError");
            }
        }

        private int age;

        public int Age
        {
            get => Age;
            set
            {
                Age = value;
                ValidateAge();
                OnPropertyChanged("Age");
            }
        }

        private string ageError;

        public string AgeError
        {
            get =>AgeError;
            set
            {
                AgeError = value;
                OnPropertyChanged("AgeError");
            }
        }

        private void ValidateAge()
        {
            // Perform validation checks
            if (Age <= 10||Age==null)
            {
                this.ShowNumofLicenseError = true;
            }
        }
        #endregion
        #region Location
        private bool showLocationError;

        public bool ShowLocationError
        {
            get => showLocationError;
            set
            {
                showLocationError = value;
                OnPropertyChanged("ShowLocationError");
            }
        }

        private string location;

        public string Location
        {
            get => Location;
            set
            {
                Location = value;
                ValidateLocation();
                OnPropertyChanged("Location");
            }
        }

        private string locationError;

        public string LocationError
        {
            get => LocationError;
            set
            {
                LocationError = value;
                OnPropertyChanged("LocationError");
            }
        }

        private void ValidateLocation()
        {
            this.ShowLocationError = string.IsNullOrEmpty(Location);
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

        //Define a command for the register button
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnRegister()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidateAge();
            ValidateUserGender();
            ValidateCompanyName();
            ValidateNumOfLicense();
            ValidateLocation();

            if (!ShowFirstNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowUserGenderError && !ShowAgeError && !ShowLocationError)//Its a casted
            {
                //Create a new AppUser object with the data from the registration form
                var newCasted = new Casted 
                {
                    UserName = FirstName,
                    UserLastName = LastName,
                    UserEmail = Email,
                    UserPassword = Password,
                    IsManager = false,
                    UserGender= UserGender,
                    UserAge=Age,
                    UserLocation= Location
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newCasted = await proxy.CastedRegister(newCasted);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newCasted != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        await proxy.LoginAsync(new Loginfo { UserEmail = newCasted.UserEmail, UserPassword = newCasted.UserPassword });
                        User? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }
                    }
                    InServerCall = false;

                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
            else if (!ShowFirstNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowCompanyNameError && !ShowNumofLicenseError)//its a castor
            {
                //Create a new AppUser object with the data from the registration form
                var newCastor = new Castor
                {
                    UserName = FirstName,
                    UserLastName = LastName,
                    UserEmail = Email,
                    UserPassword = Password,
                    IsManager = false,
                    UserGender = UserGender,
                    NumOfLisence=numOfLicense,
                    CompanyName = CompanyName
                  
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newCastor = await proxy.CastorRegister(newCastor);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newCastor != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        await proxy.LoginAsync(new Loginfo { UserEmail = newCastor.UserEmail, UserPassword = newCastor.UserPassword });
                        User? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            InServerCall = false;
                            await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }
                    }
                    InServerCall = false;

                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
        }

        //Define a method that will be called upon pressing the cancel button
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }

    }
}

