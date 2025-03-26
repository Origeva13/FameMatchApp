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
    public class MatchViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        public MatchViewModel(FameMatchWebAPIProxy proxy)
        {
            User theUser = ((App)App.Current).LoggedInUser;
            Castor castor = (Castor)theUser;
            this.proxy = proxy;

            // Initialize various properties based on available data
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
            SaveCommand = new Command(OnMatch);
            Filltered = new ObservableCollection<Casted>();
        }

        #region Height
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

        #region Filltered
        private ObservableCollection<Casted> filltered;
        public ObservableCollection<Casted> Filltered
        {
            get => this.filltered;
            set
            {
                this.filltered = value;
                OnPropertyChanged(nameof(Filltered));
            }
        }
        #endregion

        private async void ReadCasteds()
        {
            List<Casted> list = await proxy.GetAllCasteds();
            this.Filltered = new ObservableCollection<Casted>(list);
        }

        #region Single Selection
        private Casted selectedCasted;
        public Casted SelectedCasted
        {
            get
            {
                return this.selectedCasted;
            }
            set
            {
                this.selectedCasted = value;
                OnSingleSelecCasted(selectedCasted);
                OnPropertyChanged();
            }
        }

        private async void OnSingleSelecCasted(Casted p)
        {
            if (p != null)
            {
                // Pass the selectedCasted object as a navigation parameter
                var navParam = new Dictionary<string, object>
        {
            { "selectedCasted", p }  // Key: "selectedCasted", Value: the Casted object
        };

                // Navigate to MatchDetails page with the selectedCasted object
                await Shell.Current.GoToAsync("matchdetailsview", navParam);

                // Optionally, reset the selectedCasted to null after navigating
                SelectedCasted = null;
            }
        }
        #endregion

        // Command for the Save button
        public Command SaveCommand { get; }

        // Method called when the register button is clicked
        public async void OnMatch()
        {
            InServerCall = true;
            List<Casted>? AllCasteds = await proxy.GetAllCasteds();

            if (AllCasteds != null)
            {
                List<Casted> filteredCasteds = AllCasteds.Where(
                    casted =>
                    casted != null &&
                    casted.UserAge == Age &&
                    casted.UserGender == Gender ||
                    casted.UserBody == BodyStructure ||
                    casted.UserEyes == Eyes ||
                    casted.UserHair == Hair ||
                    casted.UserSkin == Skin ||
                    casted.UserHigth == Hight
                ).ToList();

                foreach (Casted casted in filteredCasteds)
                {
                    Filltered.Add(casted);
                }


                if (filteredCasteds.Any()) // Check if there are any casteds after filtering
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Match", $"{filteredCasteds.Count} matches found!", "ok");
                    //ReadCasteds();
                }
                else
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Match", "No matching casteds found.", "ok");
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

