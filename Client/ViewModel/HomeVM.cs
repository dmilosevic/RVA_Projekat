using Client.Commands;
using Client.Proxy;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class HomeVM : INotifyPropertyChanged
    {
        List<Device> devices = null;
        List<Substation> substations = null;
        List<Measurement> measurements = null;


        public event PropertyChangedEventHandler PropertyChanged;
        public User CurrentUser { get; }

        public List<Substation> Substations
        {
            get
            {
                return substations;
            }
            set
            {
                substations = value;
                selectedDevice = null;
                //PropertyChanged(this, new PropertyChangedEventArgs(nameof(Substations)));
            }
        }

        public List<Device> Devices
        {
            get
            { 
                return devices;
            }
            set
            {
                devices = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Devices)));
            }
        }
        public List<Measurement> Measurements
        {
            get
            {
                return measurements;
            }
            set
            {
                measurements = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Measurements)));
            }
        }

        public Substation selectedSubstation { get; set; }
        public Device selectedDevice { get; set; }


        //commands
        public SubstationSelectionChanged selectedSubstationChangedCmd { get; set; }
        public DeviceSelectionChanged  selectedDeviceChangedCmd { get; set; }

        public HomeVM(User loggedInUser)
        {
            CurrentUser = loggedInUser;

            RefreshData();
        }

        /// <summary>
        /// FOR TESTING PURPOSES, DELETE AFTERWARDS
        /// </summary>
        public HomeVM()
        {
            selectedSubstationChangedCmd = new SubstationSelectionChanged(this);
            selectedDeviceChangedCmd = new DeviceSelectionChanged(this);

            CurrentUser = UserProxy.Instance.Proxy.Login("admin", "admin");
            
            RefreshData();           
        }

        public void RefreshData()
        {
            Substations = DataProxy.Instance.Proxy.GetAllSubstations();

            if (selectedSubstation != null)
            {
                Devices = DataProxy.Instance.Proxy.GetDevices(selectedSubstation);
            }

            if (selectedDevice != null)
            {
                Measurements = DataProxy.Instance.Proxy.GetMeasurements(selectedDevice);
            }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
