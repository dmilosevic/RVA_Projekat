using Client.Proxy;
using Client.ViewModel;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Commands
{
    public class CloneSubstationCmd : BaseCommand
    {
        HomeVM homeVM = null;
        public CloneSubstationCmd(HomeVM vm)
        {
            homeVM = vm;
        }

        public override void Execute(object parameter)
        {
            Substation subs = homeVM.selectedSubstation;

            if (subs == null)
            {
                MessageBox.Show("Please select substation to clone", "Warning");
                return;
            }

            Substation clone = subs.Clone();

            bool success = DataProxy.Instance.Proxy.AddSubstation(clone);

            if (!success)
            {
                LoginVM.Log.Error($"Substation clone could not be added. CloneId=('{clone.Id}')");
                return;
            }
            else
            {
                LoginVM.Log.Info($"Substation clone added successfuly. CloneId=('{clone.Id}')");
            }
        }
    }
}
