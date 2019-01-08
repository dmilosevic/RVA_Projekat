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
    class AddDevice : BaseCommand
    {
        AddDeviceVM viewModel = null;

        public AddDevice(AddDeviceVM vm)
        {
            viewModel = vm;
        }

        public override void Execute(object parameter)
        {
            if (viewModel.Id == null || viewModel.Id == "")
                return;

            if (viewModel.Name == null || viewModel.Name == "")
                return;

            if (viewModel.SelectedSub == null || viewModel.SelectedSub.Name == "")
                return;

            Device newDevice = new Device()
            {
                 Id = viewModel.Id,
                  Name=viewModel.Name,
                   Device_Substation = viewModel.SelectedSub.Id,
            };

            bool success = DataProxy.Instance.Proxy.AddDevice(newDevice);

            if (!success)
            {
                LoginVM.Log.Error($"Tried to add duplicate Device. Id=('{newDevice.Id}')");
                return;
            }
            else
            {
                LoginVM.Log.Info($"Device added successfuly. Id=('{newDevice.Id}')");

                viewModel.homeVM.RefreshData();
                viewModel.view.Close();
            }
        }
    }
}
