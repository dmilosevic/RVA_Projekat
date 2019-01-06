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
    public class EditSubstationVM : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string location;

        public EditSubstation editSubstationCmd { get; set; }
        public HomeVM HomeVM { get; set; }
        public Window EditView { get; set; }

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Location)));
            }
        }
        #endregion

        public EditSubstationVM(Window view, HomeVM home)
        {
            editSubstationCmd = new EditSubstation(this);
            HomeVM = home;
            this.EditView = view;

            Id = HomeVM.selectedSubstation.Id;
            Name = HomeVM.selectedSubstation.Name;
            Location = HomeVM.selectedSubstation.Location;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
