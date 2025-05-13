using FameMatchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedUser1), "selectedUser1")]

    public class UserInfo : ViewModelBase
    {
       
            private User selectedUser1;

            // Property to hold the selected Casted
            public User SelectedUser1
            {
                get => selectedUser1;
                set
                {
                    selectedUser1 = value;
                    OnPropertyChanged();  // Notify the UI when the SelectedCasted is set
                    LoadCastedDetails();  // Optionally, load any additional details or process the data
                }
            }
        #region properties
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
        private bool isBlocked;
        public bool IsBlocked
        {
            get => isBlocked;
            set
            {
                isBlocked = value;
                OnPropertyChanged();
            }
        }
        private bool isManger;
        public bool IsManger
        {
            get => isManger;
            set
            {
                isManger = value;
                OnPropertyChanged();
            }
        }
        private bool isCasted;
        public bool IsCasted
        {
            get => isCasted;
            set
            {
                isCasted = value;
                OnPropertyChanged();
            }
        }
        private bool isCastor;
        public bool IsCastor
        {
            get => isCastor;
            set
            {
                isCastor = value;
                OnPropertyChanged();
            }
        }
        #endregion
        // Constructor 
        public UserInfo()
            {
            
            }
        //לשאול את עופר
        //public bool IsManager => SelectedUser1?.IsManager ?? false;

        //public bool IsCasted => SelectedUser1 is Casted;

        //public bool IsCastor => SelectedUser1 is Castor;

        private void LoadCastedDetails()
            {
                if (SelectedUser1 != null)
                {
                    Name = SelectedUser1.UserName;  // Assume the Casted class has a Name property
                    LastName = SelectedUser1.UserLastName;        // Assume Casted has a Bio property
                    Email = SelectedUser1.UserEmail;
                   IsBlocked = SelectedUser1.IsBlocked;
                   IsManger = SelectedUser1.IsManager;
                if (SelectedUser1 is Castor)//לתקן
                { 
                    IsCasted = true;
                    IsCastor = false;
                }
                else//לתקן
                {
                    IsCastor = true;
                    IsCasted = false;
                }
                // Optionally, load any additional details or process the data

            }
        }
        }
    }



