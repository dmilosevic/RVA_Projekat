using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Commands.OpenDialogs
{
    public class OpenEditSubstation : BaseCommand
    {
        public HomeVM viewModel = null;

        public override void Execute(object parameter)
        {
            if (viewModel.selectedSubstation == null)
            {
                MessageBox.Show("Select substation to edit");
                return;
            }

            View.EditSubstation window = new View.EditSubstation(viewModel);
            window.ShowDialog();
        }

        public OpenEditSubstation(HomeVM home)
        {
            this.viewModel = home;
        }
    }
}
