using Client.Commands;
using Client.Proxy;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    class AddMeasurementVM
    {
        public HomeVM homeVM = null;
        public Window view = null;

        public string Type { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public DateTime SelectedDate { get; set; }
        //public DateTime Today { get; set; }

        public List<Device> Devs { get; set; }
        public Device SelectedDev { get; set; }

        public AddMeasurement addMeasurementCmd { get; set; }

        public AddMeasurementVM(Window view, HomeVM home)
        {
            homeVM = home;
            this.view = view;
            Devs = DataProxy.Instance.Proxy.GetAllDevices();
            SelectedDate = DateTime.Today;

            addMeasurementCmd = new AddMeasurement(this);
        }
    }
}
