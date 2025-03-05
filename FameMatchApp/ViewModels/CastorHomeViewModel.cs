using FameMatchApp.Models;
using FameMatchApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.ViewModels
{
    public class CastorHomeViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        public CastorHomeViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            User theUser = ((App)App.Current).LoggedInUser;
            Castor castor = (Castor)theUser;
            IsPublicCommand= new Command(OnPublic);
            IsntPublicCommand = new Command(OnNotPublic);
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
        public Command IsPublicCommand { get; }
        public Command IsntPublicCommand { get; }
        public async void OnPublic()//להוסיף
        {

        }
        public async void OnNotPublic()//להוסיף
        {

        }


    }

}

