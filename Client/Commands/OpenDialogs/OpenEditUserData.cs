using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenEditUserData : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.EditUserData window = new View.EditUserData(viewModel);
            window.ShowDialog();
        }

        public OpenEditUserData(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
