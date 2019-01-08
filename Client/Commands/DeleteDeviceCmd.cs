using Client.Proxy;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Commands
{
    public class DeleteDeviceCmd : BaseCommand
    {
        HomeVM homeVM = null;

        public DeleteDeviceCmd(HomeVM vm)
        {
            homeVM = vm;
        }
        public override void Execute(object parameter)
        {
            if (homeVM.selectedDevice == null || homeVM.selectedDevice.Id == "")
            {
                MessageBox.Show("Select device to delete", "Info");
                return;
            }

            DataProxy.Instance.Proxy.DeleteDevice(homeVM.selectedDevice.Id);
            LoginVM.Log.Info($"Device deleted. Id=('{homeVM.selectedDevice.Id}')");

            homeVM.RefreshData();

        }
    }
}
