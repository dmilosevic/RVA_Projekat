using Client.Commands;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    class AddDeviceVM
    {
        public HomeVM homeVM = null;
        public Window view = null;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Substation { get; set; }
        public List<Substation> Subs { get; set; }
        public Substation SelectedSub { get; set; }

        public AddDevice addDeviceCmd { get; set; }

        public AddDeviceVM(Window view, HomeVM home)
        {
            homeVM = home;
            this.view = view;
            Subs = homeVM.Substations;

            addDeviceCmd = new AddDevice(this);
        }
    }
}
