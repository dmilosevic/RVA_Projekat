using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenAddUser : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.AddUser window = new View.AddUser(viewModel);
            window.ShowDialog();
        }

        public OpenAddUser(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
