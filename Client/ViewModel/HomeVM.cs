using Client.Commands;
using Client.Commands.OpenDialogs;
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
    public class HomeVM : INotifyPropertyChanged
    {
        List<Device> devices = null;
        List<Substation> substations = null;
        List<Measurement> measurements = null;


        //PROPERTIES
        #region Undo/Redo properties
        public List<BaseCommand> UndoHistory { get; set; }
        public List<BaseCommand> RedoHistory { get; set; }

        public List<Substation> SubstationsUndo { get; set; }
        public List<Device> DevicesUndo { get; set; }
        public List<Measurement> MeasurementsUndo { get; set; }

        public List<Substation> SubstationsRedo { get; set; }
        public List<Device> DevicesRedo { get; set; }
        public List<Measurement> MeasurementsRedo { get; set; }
        #endregion

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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Substations)));
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Devices)));
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Measurements)));
            }
        }

        public Substation selectedSubstation { get; set; }
        public Device selectedDevice { get; set; }


        //commands
        public SubstationSelectionChanged selectedSubstationChangedCmd { get; set; }
        public DeviceSelectionChanged  selectedDeviceChangedCmd { get; set; }
        public AddSubstationDialog openAddSubstationDialogCmd { get; set; }
        public DeleteSubstation deleteSubstationCmd { get; set; }
        public RedoCommand redoCmd { get; set; }
        public UndoCommand undoCmd { get; set; }

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
            openAddSubstationDialogCmd = new AddSubstationDialog(this);
            deleteSubstationCmd = new DeleteSubstation(this);
            redoCmd = new RedoCommand(this);
            undoCmd = new UndoCommand(this);

            #region initialize Undo/Redo data holders
            RedoHistory = new List<BaseCommand>();
            UndoHistory = new List<BaseCommand>();

            SubstationsUndo = new List<Substation>();
            DevicesUndo = new List<Device>();
            MeasurementsUndo = new List<Measurement>();

            SubstationsRedo = new List<Substation>();
            DevicesRedo = new List<Device>();
            MeasurementsRedo = new List<Measurement>();
            #endregion


            CurrentUser = UserProxy.Instance.Proxy.Login("admin", "admin");
            
            RefreshData();           
        }

        public void RefreshData()
        {
            Substations = DataProxy.Instance.Proxy.GetAllSubstations();

            Devices = selectedSubstation != null ? DataProxy.Instance.Proxy.GetDevices(selectedSubstation) : null;

            Measurements = selectedDevice != null ? DataProxy.Instance.Proxy.GetMeasurements(selectedDevice) : null;
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
