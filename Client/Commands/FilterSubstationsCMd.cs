using Client.Proxy;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class FilterSubstationsCmd : BaseCommand
    {
        HomeVM homeVM = null;

        public FilterSubstationsCmd(HomeVM vm)
        {
            homeVM = vm;
        }

        public override void Execute(object parameter)
        {
            homeVM.Substations = DataProxy.Instance.Proxy.GetAllSubstations();
            string name = homeVM.FilterName.ToLower();
            string location = homeVM.FilterLocation.ToLower();

            homeVM.Substations = homeVM.Substations.Where(s => s.Location.ToLower().Contains(location) &&
                                                               s.Name.ToLower().Contains(name)).ToList();
        }
    }
}
