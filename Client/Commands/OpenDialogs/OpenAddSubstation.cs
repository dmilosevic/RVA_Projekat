using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenAddSubstation : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.AddSubstation window = new View.AddSubstation(viewModel);
            window.ShowDialog();
        }

        public OpenAddSubstation(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
