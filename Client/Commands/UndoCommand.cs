using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Commands
{
    public class UndoCommand : BaseCommand
    {
        HomeVM viewModel;
        public UndoCommand(HomeVM homevm)
        {
            this.viewModel = homevm;
        }

        public override void Execute(object parameter)
        {
            if(viewModel.UndoHistory.Count <= 0)
            {
                //MessageBox.Show("nema nsita za UNDO");
                LoginVM.Log.Warn("UNDO commands list is empty");
                return;
            }

            BaseCommand cmd = viewModel.UndoHistory[viewModel.UndoHistory.Count - 1]; //get last item
            cmd.UnExecute();
            viewModel.UndoHistory.Remove(cmd);
            LoginVM.Log.Info("UNDO command invoked");
            //viewModel.SubstationsUndo.RemoveAt(viewModel.SubstationsUndo.Count - 1);
        }
    }
}
