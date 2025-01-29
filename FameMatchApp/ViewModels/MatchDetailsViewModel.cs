using FameMatchApp.Models;
using System;

namespace FameMatchApp.ViewModels
{
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

        // Constructor to accept the selectedCasted
        public MatchDetailsViewModel(Casted selectedCasted)
        {
            SelectedCasted = selectedCasted;
        }

        private void LoadCastedDetails()
        {
            // Here, you can process any logic if necessary when the Casted object is set
            // For example, load additional details or perform calculations based on the casted data
        }
    }
}
