﻿using System;
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
            userGenderError = "Please choose Gender";
            CompanyNameError = "Company Name is required";
            
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

            if (!ShowFirstNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError)
            {
                //Create a new AppUser object with the data from the registration form
                var newUser = new AppUser
                {
                    UserName = FirstName,
                    UserLastName = LastName,
                    UserEmail = Email,
                    UserPassword = Password,
                    IsManager = false
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newUser = await proxy.Register(newUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        await proxy.LoginAsync(new LoginInfo { Email = newUser.UserEmail, Password = newUser.UserPassword });
                        AppUser? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
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

