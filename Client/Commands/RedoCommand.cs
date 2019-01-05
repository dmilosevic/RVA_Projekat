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
    public class RedoCommand : BaseCommand
    {
        HomeVM viewModel;
        public RedoCommand(HomeVM homevm)
        {
            this.viewModel = homevm;
        }

        public override void Execute(object parameter)
        {
            if (viewModel.RedoHistory.Count <= 0)
            {
                MessageBox.Show("Nema nsita za redo");
                return;
            }

            BaseCommand cmd = viewModel.RedoHistory[viewModel.RedoHistory.Count - 1]; //get last item
            Substation subs = viewModel.SubstationsRedo[viewModel.SubstationsRedo.Count - 1];

            //Mozda napraviti apstraktnu klasu za sve modele, i u njoj apstraktnu metodu GetParams. 
            //Kako ne bih pravio undo/redo komande za svaki model

            //za sad nek ide ovako
            object[] parameters = new object[3];
            parameters[0] = subs.Name;
            parameters[1] = subs.Location;
            parameters[2] = subs.Id;

            cmd.Execute(parameters);

            viewModel.RedoHistory.Remove(cmd);
            viewModel.SubstationsRedo.RemoveAt(viewModel.SubstationsRedo.Count - 1);

            viewModel.RefreshData();
        }
    }
}
