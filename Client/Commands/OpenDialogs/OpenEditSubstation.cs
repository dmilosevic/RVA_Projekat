using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenEditSubstation : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.EditSubstation window = new View.EditSubstation(viewModel);
            window.ShowDialog();
        }

        public OpenEditSubstation(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
