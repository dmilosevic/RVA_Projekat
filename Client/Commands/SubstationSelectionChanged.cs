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
    class SubstationSelectionChanged : BaseCommand
    {
        HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;

            Substation selectedSub = parameter as Substation;

            if (selectedSub == null)
                return;

            viewModel.Devices = DataProxy.Instance.Proxy.GetDevices(selectedSub);
            viewModel.Measurements = null; //clear measurements when substation changes
        }

        public SubstationSelectionChanged(HomeVM vm)
        {
            viewModel = vm;
        }
    }
}
