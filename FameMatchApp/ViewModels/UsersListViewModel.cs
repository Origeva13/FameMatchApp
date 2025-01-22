
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FameMatchApp.Services;
using FameMatchApp.Models;

namespace FameMatchApp.ViewModels
{
    public class UsersListViewModel : ViewModelBase
    {
        private FameMatchWebAPIProxy proxy;
        public UsersListViewModel(FameMatchWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Users = new ObservableCollection<User>();

            ReadUsers();
        }
        #region Collection View of Users
        private List<User> allUsers;
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
                OnPropertyChanged(nameof(Users));
            }
        }


        private async void ReadUsers()
        {
            List<User> list = await proxy.GetUsers();
            this.Users = new ObservableCollection<User>(list);
            allUsers = list;
        }


        #region Single Selection


        private User selectedUser;
        public User SelectedUser
        {
            get
            {
                return this.selectedUser;
            }
            set
            {
                this.selectedUser = value;
                OnSingleSelectUser(selectedUser);
                OnPropertyChanged();
            }
        }



        private async void OnSingleSelectUser(User p)
        {
            if (p != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedUser",p }
                };
                await Shell.Current.GoToAsync("ProfileView", navParam);

                SelectedUser = null;

            }
        }
        #endregion
        #endregion

        public void Sort()
        {
            if (UserName != null || UserName != "")
            {
                List<User> temp = allUsers.Where(u => u.UserName.Contains(UserName)).ToList();
                Users = new ObservableCollection<User>(temp);
            }
            else
            {
                Users = new ObservableCollection<User>(allUsers);
            }

        }
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                //Sort();
                OnPropertyChanged("UserName");
                Sort();
            }
        }
    }
}
