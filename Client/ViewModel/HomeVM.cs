﻿using Client.Commands;
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
using System.Windows;

namespace Client.ViewModel
{
    public class HomeVM : INotifyPropertyChanged
    {
        public static RefreshCommand refreshCommand { get; set; }
        
        

        List<Device> devices = null;
        List<Substation> substations = null;
        List<Measurement> measurements = null;
        string filterName = null;
        string filterLocation = null;

        User currentuser = null;
        public string visibleIfAdmin { get; set; }
        public Window View { get; set; }

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

        #region Binding Properties
        public User CurrentUser
        {
            get
            {
                return currentuser;
            }
            set
            {
                currentuser = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentUser)));
            }
        }

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

        

        public string FilterName
        {
            get { return filterName; }
            set
            {
                filterName = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FilterName)));
            }
        }

        public string FilterLocation
        {
            get { return filterLocation; }
            set
            {
                filterLocation = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FilterLocation)));
            }
        }

        #endregion


        public Substation selectedSubstation { get; set; }
        public Device selectedDevice { get; set; }
        public Measurement selectedMeasurement { get; set; }


        //commands
        public SubstationSelectionChanged selectedSubstationChangedCmd { get; set; }
        public DeviceSelectionChanged  selectedDeviceChangedCmd { get; set; }
        public OpenAddSubstation openAddSubstationDialogCmd { get; set; }
        public OpenEditSubstation openEditSubstationCmd { get; set; }
        public DeleteSubstation deleteSubstationCmd { get; set; }
        public SignOutCommand signOutCmd { get; set; }
        public OpenAddUser openAddUserCmd { get; set; }
        public OpenEditUserData openEditUserDataCmd { get; set; }
        public CloneSubstationCmd cloneSubstationCmd { get; set; }
        public FilterSubstationsCmd filterSubstationCmd { get; set; }
        public OpenAddDevice openAddDeviceCmd { get; set; }
        public DeleteDeviceCmd deleteDeviceCmd { get; set; }
        public OpenAddMeasurement openAddMeasurementCmd { get; set; }
        public DeleteMeasurement deleteMeasurementCmd { get; set; }

        public RedoCommand redoCmd { get; set; }
        public UndoCommand undoCmd { get; set; }

        public HomeVM(User loggedInUser, Window view)
        {
            //System.Diagnostics.PresentationTraceSources.SetTraceLevel(this.View.dataGrid.ItemContainerGenerator, System.Diagnostics.PresentationTraceLevel.High);
            CurrentUser = loggedInUser;
            this.View = view;

            //InMemoryAppender.LogData

            selectedSubstationChangedCmd = new SubstationSelectionChanged(this);
            selectedDeviceChangedCmd = new DeviceSelectionChanged(this);
            openAddSubstationDialogCmd = new OpenAddSubstation(this);
            deleteSubstationCmd = new DeleteSubstation(this);
            redoCmd = new RedoCommand(this);
            undoCmd = new UndoCommand(this);
            openEditSubstationCmd = new OpenEditSubstation(this);
            refreshCommand = new RefreshCommand(this);
            signOutCmd = new SignOutCommand(this);
            openAddUserCmd = new OpenAddUser(this);
            openEditUserDataCmd = new OpenEditUserData(this);
            cloneSubstationCmd = new CloneSubstationCmd(this);
            filterSubstationCmd = new FilterSubstationsCmd(this);
            openAddDeviceCmd = new OpenAddDevice(this);
            deleteDeviceCmd = new DeleteDeviceCmd(this);
            openAddMeasurementCmd = new OpenAddMeasurement(this);
            deleteMeasurementCmd = new DeleteMeasurement(this);

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

            FilterName = "";
            FilterLocation = "";

            //CurrentUser = UserProxy.Instance.Proxy.Login("admin", "admin");
            visibleIfAdmin = CurrentUser.isAdmin ? "Visible" : "Hidden"; // show/hide GUI elements based on priviledge

            RefreshData();
        }

        ///// <summary>
        ///// FOR TESTING PURPOSES, DELETE AFTERWARDS
        ///// </summary>
        //public HomeVM()
        //{
            

        //    selectedSubstationChangedCmd = new SubstationSelectionChanged(this);
        //    selectedDeviceChangedCmd = new DeviceSelectionChanged(this);
        //    openAddSubstationDialogCmd = new OpenAddSubstation(this);
        //    deleteSubstationCmd = new DeleteSubstation(this);
        //    redoCmd = new RedoCommand(this);
        //    undoCmd = new UndoCommand(this);
        //    openEditSubstationCmd = new OpenEditSubstation(this);

        //    #region initialize Undo/Redo data holders
        //    RedoHistory = new List<BaseCommand>();
        //    UndoHistory = new List<BaseCommand>();

        //    SubstationsUndo = new List<Substation>();
        //    DevicesUndo = new List<Device>();
        //    MeasurementsUndo = new List<Measurement>();

        //    SubstationsRedo = new List<Substation>();
        //    DevicesRedo = new List<Device>();
        //    MeasurementsRedo = new List<Measurement>();
        //    #endregion


        //    CurrentUser = UserProxy.Instance.Proxy.Login("admin", "admin");
        //    visibleIfAdmin = CurrentUser.isAdmin ? "Visible" : "Hidden"; // show/hide GUI elements based on priviledge

        //    RefreshData();           
        //}

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
