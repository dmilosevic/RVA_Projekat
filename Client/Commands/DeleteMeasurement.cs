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
    public class DeleteMeasurement : BaseCommand
    {
        HomeVM homeVM = null;

        public DeleteMeasurement(HomeVM vm)
        {
            homeVM = vm;
        }
        public override void Execute(object parameter)
        {
            if (homeVM.selectedMeasurement == null)
            {
                MessageBox.Show("Select measurement to delete", "Info");
                return;
            }

            DataProxy.Instance.Proxy.DeleteMeasurement(homeVM.selectedMeasurement.Id);

            homeVM.RefreshData();

        }
    }
}
