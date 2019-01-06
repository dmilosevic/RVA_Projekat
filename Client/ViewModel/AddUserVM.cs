using Client.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    class AddUserVM : INotifyPropertyChanged
    {
        private string username, password, firstname, lastname;
        private bool isAdmin;

        public Window View { get; set; }
        public HomeVM HomeVM { get; set; }

        public AddUserCmd addUserCmd { get; set; }

        #region Properties
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public string FirstName
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }

        public string LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsAdmin)));
            }
        }
        #endregion



        public AddUserVM(Window view, HomeVM home)
        {
            this.View = view;
            this.HomeVM = home;

            addUserCmd = new AddUserCmd(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
