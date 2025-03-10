using FameMatchApp.Models;
using FameMatchApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.ViewModels
{
    public class CastorHomeViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        private Castor castor;
        public CastorHomeViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            User theUser = ((App)App.Current).LoggedInUser;
            this.castor = (Castor)theUser;
            IsPublicCommand= new Command<Audition>(OnPublic);
            IsntPublicCommand = new Command<Audition>(OnNotPublic);
            UsersAud = new ObservableCollection<Audition>();
            ReadAuditions();
        }
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
        #region UsersAud
        private ObservableCollection<Audition> usersAud;
        public ObservableCollection<Audition> UsersAud
        {
            get => this.usersAud;
            set
            {
                this.usersAud = value;
                OnPropertyChanged(nameof(UsersAud));
            }
        }

        private async void ReadAuditions()
        {
            List<Audition> list = await proxy.GetUserAudition(castor.UserId);
            this.UsersAud = new ObservableCollection<Audition>(list);
        }
#endregion
        public Command IsPublicCommand { get; }
        public Command IsntPublicCommand { get; }
        public async void OnPublic(Audition aud)//להוסיף
        {
                aud.IsPublic = true;
                await proxy.UpdateAudition(aud);
               InServerCall = false;
               await Shell.Current.DisplayAlert("Audition", $"The audition is now public!", "ok");//לתקן
        }
    
        public async void OnNotPublic(Audition aud)//להוסיף
        {
            aud.IsPublic = false;
            await proxy.UpdateAudition(aud);
            InServerCall = false;
            await Shell.Current.DisplayAlert("Audition", $"The audition is now not public!", "ok");//לתקן
        }
    }

}

