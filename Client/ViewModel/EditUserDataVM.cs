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
    class EditUserDataVM : INotifyPropertyChanged
    {
        public Window View { get; set; }
        public HomeVM homeVM = null;

        public EditUserDataCmd editUserCmd { get; set; }

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;                
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Firstname)));
            }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Lastname)));
            }
        }



        public EditUserDataVM(Window view, HomeVM home)
        {
            this.View = view;
            this.homeVM = home;

            Username = homeVM.CurrentUser.Username;
            Firstname = homeVM.CurrentUser.FirstName;
            Lastname = home.CurrentUser.LastName;

            editUserCmd = new EditUserDataCmd(this, view);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
