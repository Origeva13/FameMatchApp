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
    public class AuditionListViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        public AuditionListViewModel(FameMatchWebAPIProxy proxy)
        {
            User theUser = ((App)App.Current).LoggedInUser;
            Casted casted = (Casted)theUser;
            Audition audition = new Audition();

            this.proxy = proxy;
            Eyes=casted.UserEyes;
            Body = casted.UserBody;
            Hight = casted.UserHigth;
            Skin = casted.UserSkin;
            Hair=casted.UserHair;
            Age = casted.UserAge;
            Gender = casted.UserGender;
            Filltered = new ObservableCollection<Audition>();
            SaveCommand=new Command(OnMatch);

        }
        private string eyes;
        public string Eyes
        {
            get => eyes;
            set
            {
                eyes = value;
                OnPropertyChanged();
            }
        }
        private string body;
        public string Body
        {
            get => body;
            set
            {
                body = value;
                OnPropertyChanged();
            }
        }
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
        private string skin;
        public string Skin
        {
            get => skin;
            set
            {
                skin = value;
                OnPropertyChanged();
            }
        }
        private string hair;
        public string Hair
        {
            get => hair;
            set
            {
                hair = value;
                OnPropertyChanged();
            }
        }
        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged();
            }
        }
        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }
        #region Filltered
        private ObservableCollection<Audition> filltered;
        public ObservableCollection<Audition> Filltered
        {
            get => this.filltered;
            set
            {
                this.filltered = value;
                OnPropertyChanged(nameof(Filltered));
            }
        }
        #endregion
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
        private async void ReadAuditions()
        {
            List<Audition> list = await proxy.GetAllAuditions();
            this.Filltered = new ObservableCollection<Audition>(list);
        }

        #region Single Selection
        private Audition selectedAudition;
        public Audition SelectedAudition
        {
            get
            {
                return this.selectedAudition;
            }
            set
            {
                this.selectedAudition = value;
                OnSingleSelecAudition(selectedAudition);
                OnPropertyChanged();
            }
        }

        private async void OnSingleSelecAudition(Audition a)
        {
            if (a != null)
            {
                // Pass the selectedCasted object as a navigation parameter
                var navParam = new Dictionary<string, object>
        {
            { "selectedAudition", a }  // Key: "selectedCasted", Value: the Casted object
        };

                // Navigate to MatchDetails page with the selectedCasted object
                await Shell.Current.GoToAsync("auditioninfoview", navParam);

                // Optionally, reset the selectedCasted to null after navigating
                SelectedAudition = null;
            }
        }
        #endregion

        // Command for the Save button
        public Command SaveCommand { get; }

        // Method called when the register button is clicked
        public async void OnMatch()
        {
            InServerCall = true;
            List<Audition>? AllAuditions = await proxy.GetAllAuditions();

            if (AllAuditions != null)
            {
                List<Audition> filteredAudition = AllAuditions.Where(
                    audition =>
                    audition != null &&
                    audition.IsPublic == true &&
                    audition.AudAge == Age &&
                    audition.AudGender == Gender ||
                    audition.UserBody == Body ||
                    audition.AudEyes == Eyes ||
                    audition.AudHair == Hair ||
                    audition.AudSkin == Skin ||
                    audition.AudHigth == Hight
                ).ToList();

                foreach (Audition audition in filteredAudition)
                {
                    Filltered.Add(audition);
                }


                if (filteredAudition.Any()) // Check if there are any casteds after filtering
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Match", $"{filteredAudition.Count} Auditions found based on your details!", "ok");
                    //ReadCasteds();
                }
                else
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Match", "No matching auditions found.", "ok");
                }
            }
            else
            {
                InServerCall = false;
                // If the registration failed, display an error message
                string errorMsg = "Match failed. Please try again.";
                await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
            }
        }
    }
}

