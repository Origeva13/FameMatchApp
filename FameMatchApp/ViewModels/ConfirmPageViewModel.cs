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
    public class ConfirmPageViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;

        public ConfirmPageViewModel(FameMatchWebAPIProxy proxy)
        {
            User theUser = ((App)App.Current).LoggedInUser;
            this.proxy = proxy;
            OnMatch();
            Filltered = new ObservableCollection<Castor>();
            IsRefreshing = false;
            RefreshingCommand = new Command(OnRefreshing);
        }

        #region Filltered
        private ObservableCollection<Castor> filltered;
        public ObservableCollection<Castor> Filltered
        {
            get => this.filltered;
            set
            {
                this.filltered = value;
                OnPropertyChanged(nameof(Filltered));
            }
        }
        #endregion

        #region Single Selection
        private Castor selectedCastor;
        public Castor SelectedCastor
        {
            get
            {
                return this.selectedCastor;
            }
            set
            {
                this.selectedCastor = value;
                OnSingleSelecCasted(selectedCastor);
                OnPropertyChanged();
            }
        }

        private async void OnSingleSelecCasted(Castor p)
        {
            if (p != null)
            {
                // Pass the selectedCasted object as a navigation parameter
                var navParam = new Dictionary<string, object>
                {
                    { "selectedCastor", p }  // Key: "selectedCasted", Value: the Casted object
                };

                // Navigate to MatchDetails page with the selectedCasted object
                await Shell.Current.GoToAsync("CastorInfo", navParam);   // To be updated with actual route name

                // Optionally, reset the selectedCasted to null after navigating
                SelectedCastor = null;
            }
        }
        #endregion
        public async void OnMatch()
        {
            InServerCall = true;
            List<Castor>? AllCastors = await proxy.GetAllCastors();

            if (AllCastors != null)
            {
                // Clear previous data before adding new filtered data
                Filltered.Clear();

                // Apply the filter to only include unapproved (IsApproved == false or null) castors
                List<Castor> filteredCastors = AllCastors.Where(castor => castor.IsAprooved == false && castor.IsBlocked==true).ToList();

                // Add the filtered castors to the observable collection
                foreach (Castor castor in filteredCastors)
                {
                    Filltered.Add(castor);
                }

                // Display a message based on whether any unapproved users were found
                if (filteredCastors.Any())
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Confirm", $"{filteredCastors.Count} Not Approved Users found!", "OK");
                }
                else
                {
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Confirm", "Not Approved Users wasnt found.", "OK");
                }

                // Debugging: Print the count of filtered castors
                Console.WriteLine($"Filtered castors count: {Filltered.Count}");
            }
            else
            {
                InServerCall = false;
                // If the registration failed, display an error message
                string errorMsg = "Confirm failed. Please try again.";
                await Shell.Current.DisplayAlert("Save ", errorMsg, "OK");
            }
        }

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
            OnMatch();
            IsRefreshing = false;
        }

    }
    #endregion
}


