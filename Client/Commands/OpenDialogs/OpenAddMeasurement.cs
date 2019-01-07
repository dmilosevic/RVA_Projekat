using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenAddMeasurement : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.AddMeasurement window = new View.AddMeasurement(viewModel);
            window.ShowDialog();
        }

        public OpenAddMeasurement(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
