using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenAddDevice : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.AddDevice window = new View.AddDevice(viewModel);
            window.ShowDialog();
        }

        public OpenAddDevice(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
