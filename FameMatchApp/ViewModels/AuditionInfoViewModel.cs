using FameMatchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedAudition), "selectedAudition")]

    public class AuditionInfoViewModel : ViewModelBase
    {

        private Audition selectedAudition;

        // Property to hold the selected Casted
        public Audition SelectedAudition
        {
            get => selectedAudition;
            set
            {
                selectedAudition = value;
                OnPropertyChanged();  // Notify the UI when the SelectedCasted is set
                LoadCastedDetails();  // Optionally, load any additional details or process the data
            }
        }
        private string audname1;
        public string AudName1
        {
            get => audname1;
            set
            {
                audname1 = value;
                OnPropertyChanged();
            }
        } 
        private string description1;
        public string Description1
        {
            get => description1;
            set
            {
                description1 = value;
                OnPropertyChanged();
            }
        }
        // Constructor 
        public AuditionInfoViewModel()
        {

        }
        //לשאול את עופר
        //public bool IsManager => SelectedUser1?.IsManager ?? false;

        //public bool IsCasted => SelectedUser1 is Casted;

        //public bool IsCastor => SelectedUser1 is Castor;

        private void LoadCastedDetails()
        {
            if (SelectedAudition != null)
            {
                AudName1 = SelectedAudition.AudName;  // Assume the Casted class has a Name property
                Description1 = SelectedAudition.Description;


                // Optionally, load any additional details or process the data

            }
        }
    }
}

