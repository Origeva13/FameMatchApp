using FameMatchApp.Models;
using System;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedCasted), "selectedCasted")]
    public class MatchDetailsViewModel : ViewModelBase
    {
        private Casted selectedCasted;

        // Property to hold the selected Casted
        public Casted SelectedCasted
        {
            get => selectedCasted;
            set
            {
                selectedCasted = value;
                OnPropertyChanged();  // Notify the UI when the SelectedCasted is set
                LoadCastedDetails();  // Optionally, load any additional details or process the data
            }
        }
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
        // Constructor 
        public MatchDetailsViewModel()
        {

        }

        private void LoadCastedDetails()
        {
            if (SelectedCasted != null)
            {
                Name = SelectedCasted.UserName;  // Assume the Casted class has a Name property
                LastName = SelectedCasted.UserLastName;        // Assume Casted has a Bio property
                Email = SelectedCasted.UserEmail;
                AboutMe = selectedCasted.AboutMe;// Assume a URL for the casted's photo
            }
        }
    }
}
