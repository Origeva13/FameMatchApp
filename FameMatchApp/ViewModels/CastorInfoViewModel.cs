using FameMatchApp.Models;
using FameMatchApp.Services;
using System;

namespace FameMatchApp.ViewModels
{
    [QueryProperty(nameof(SelectedCastor), "selectedCastor")]

    public class CastorInfoViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;

        private Castor selectedCastor;

        // Property to hold the selected Castor
        public Castor SelectedCastor
        {
            get => selectedCastor;
            set
            {
                selectedCastor = value;
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
        private string companyName;
        public string CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChanged();
            }
        }
        private int numOFId;
        public int NumOFId
        {
            get => numOFId;
            set
            {
                numOFId = value;
                OnPropertyChanged();
            }
        }


        // Constructor 
        public CastorInfoViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy=proxy;
            AcceptCommand = new Command(OnAccept);
            DeclaineCommand = new Command(OnDeclaine);
        }
        public Command AcceptCommand { get; }

        public async void OnAccept()
        {
            selectedCastor.IsAprooved = true;
            bool isWorking = await proxy.Accept(selectedCastor);
            if (isWorking == true)
            {
                await Application.Current.MainPage.DisplayAlert("success", $"Castor has been approved", "Ok");
                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Castor hasn't been approved", "Ok");

            }
        }
        public Command DeclaineCommand { get; }

        public async void OnDeclaine()
        {
            selectedCastor.IsAprooved = false;  
            selectedCastor.IsBlocked = true;
            bool isWorking = await proxy.Declaine(selectedCastor);
            if (isWorking == true)
            {
                await Application.Current.MainPage.DisplayAlert("success", $"Castor has been declained", "Ok");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Castor hasn't been declained", "Ok");

            }
        }
        private void LoadCastedDetails()
        {
            if (SelectedCastor != null)
            {
                Name = SelectedCastor.UserName;  // Assume the Casted class has a Name property
                LastName = SelectedCastor.UserLastName;        // Assume Casted has a Bio property
                Email = SelectedCastor.UserEmail;
                CompanyName = SelectedCastor.CompanyName;
                NumOFId = SelectedCastor.NumOfLisence;
            }
        }
    }
}
