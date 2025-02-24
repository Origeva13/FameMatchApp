using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FameMatchApp.Models;
using FameMatchApp.Services;
namespace FameMatchApp.ViewModels
{
    public class AddAuditionViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        public AddAuditionViewModel(FameMatchWebAPIProxy proxy)
        {
            Audition audition = new Audition();
            User theUser = ((App)App.Current).LoggedInUser;
            Castor castor = (Castor)theUser;
            this.proxy = proxy;

            // Initialize various properties based on available data
            AuditionName = audition.AudName;
            Description = audition.Description;
            Kinds = (new BodyStructure()).Kinds;
            BodyStructure = Kinds[0];
            Kinds1 = (new Age()).Kinds1;
            Age = Kinds1[0];
            Kinds2 = (new Eyes()).Kinds2;
            Eyes = Kinds2[0];
            Kinds3 = (new Hair()).Kinds3;
            Hair = Kinds3[0];
            Kinds4 = (new Skin()).Kinds4;
            Skin = Kinds4[0];
            Kinds5 = (new Gender()).Kinds5;
            Gender = Kinds5[0];
            kinds6 = (new Hight()).Kinds6;
            Hight = Kinds6[0];
            IsPublic = false;
            UserId=castor.UserId;
            AuditionNameError= "Audition name filed can't be empty";
            SaveCommand = new Command(OnPost);
        }

        #region AuditionName
        private string auditionName;
        public string AuditionName
        {
            get => auditionName;
            set
            {
                auditionName = value;
                OnPropertyChanged("AuditionName");
            }
        }
        private bool showAuditionNameError;

        public bool ShowAuditionNameError
        {
            get => showAuditionNameError;
            set
            {
                showAuditionNameError = value;
                OnPropertyChanged("ShowAuditionNameError");
            }
        }
        private string auditionnameError;

        public string AuditionNameError
        {
            get => auditionnameError;
            set
            {
                auditionnameError = value;
                OnPropertyChanged("AuditionNameError");
            }
        }

        private void ValidateName()
        {
            this.ShowAuditionNameError = string.IsNullOrEmpty(AuditionName);
        }
        #endregion

        #region Description
        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
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
                OnPropertyChanged("Hight");
            }
        }

        private List<int> kinds6;
        public List<int> Kinds6
        {
            get => kinds6;
            set
            {
                kinds6 = value;
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

        #region gender
        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<string> kinds5;
        public List<string> Kinds5
        {
            get => kinds5;
            set
            {
                kinds5 = value;
                OnPropertyChanged();
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

        #region UserId
        private int userId;
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        #endregion


        // Command for the Save button
        public Command SaveCommand { get; }
        public async void OnPost()
        {

            if (!ShowAuditionNameError)
            {
                User theUser = ((App)App.Current).LoggedInUser;
                
                if (theUser.IsBlocked == true)
                {
                    await Application.Current.MainPage.DisplayAlert("Add Audition", "Uploading Audition failed since user is blocked", "ok");
                }
                User u = ((App)Application.Current).LoggedInUser;
                //string extention = PhotoURL.Substring(PhotoURL.LastIndexOf(".")).ToLower();
                Audition a = new Audition
                {
                    UserId = u.UserId,
                    AudName = AuditionName,
                    Description = Description,
                    AudAge = Age,
                    AudLocation = "",
                    AudHigth = Hight,
                    AudHair = Hair,
                    AudEyes = Eyes,
                    UserBody = BodyStructure,
                    AudSkin = Skin,
                    AudGender=Gender,
                    IsPublic = IsPublic

                }
                ;
                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                a = await proxy.AddAudition(a);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (a != null)
                {
                    InServerCall = false;
                    await Application.Current.MainPage.DisplayAlert("Add Audition", " Audition added successfully", "ok");
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Audition added failed";
                    await Application.Current.MainPage.DisplayAlert("Add Audition", errorMsg, "ok");
                }


            }

            else
            {
                await Shell.Current.DisplayAlert("Add Audition", "Adding Audition failed", "ok");

            }
            }

            }
        }
    
       
   
    

