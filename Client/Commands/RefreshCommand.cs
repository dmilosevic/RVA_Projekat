using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class RefreshCommand : BaseCommand
    {
        HomeVM homeVM = null;
        public RefreshCommand(HomeVM home)
        {
            this.homeVM = home;
        }
        public override void Execute(object parameter)
        {
            homeVM.RefreshData();
        }
    }
}
