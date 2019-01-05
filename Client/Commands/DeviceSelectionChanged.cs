using Client.Proxy;
using Client.ViewModel;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class DeviceSelectionChanged : BaseCommand
    {
        HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            Device selectedDevice = parameter as Device;

            if (selectedDevice == null)
                return;

            viewModel.Measurements = DataProxy.Instance.Proxy.GetMeasurements(selectedDevice);
        }

        public DeviceSelectionChanged(HomeVM vm)
        {
            viewModel = vm;
        }
    }
}
