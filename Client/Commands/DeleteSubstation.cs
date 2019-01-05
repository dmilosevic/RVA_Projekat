using Client.Proxy;
using Client.ViewModel;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class DeleteSubstation : BaseCommand
    {
        HomeVM viewModel = null;

        public DeleteSubstation(HomeVM vm)
        {
            viewModel = vm;
        }

        public override void Execute(object parameter) //SHOULD pass substation as parameter
        {
            if (parameter == null)
                return;

            //Substation subs = viewModel.selectedSubstation as Substation;
            Substation subs = parameter as Substation;
            if (subs == null)
            {
                try
                { 
                    //parse parameters sent by undo/redo command
                    object[] parameters = parameter as object[];
                    subs = new Substation()
                    {
                        Name = parameters[0].ToString(),
                        Location = parameters[1].ToString(),
                        Id = int.Parse(parameters[2].ToString()),
                    };
                }
                catch (Exception)
                {
                    return;
                }
            }

            DataProxy.Instance.Proxy.DeleteSubstation(subs.Id);
            viewModel.RefreshData();

            viewModel.UndoHistory.Add(this);
            viewModel.SubstationsUndo.Add(subs);            
        }

        public override void UnExecute()
        {
            var listSubs = viewModel.SubstationsUndo;
            if (listSubs.Count <= 0)
                return;

            Substation subs = listSubs[listSubs.Count - 1];
            DataProxy.Instance.Proxy.AddSubstation(subs);
            viewModel.RefreshData();

            viewModel.SubstationsUndo.RemoveAt(viewModel.SubstationsUndo.Count - 1);
            viewModel.RedoHistory.Add(this);
            viewModel.SubstationsRedo.Add(subs);
        }
    }
}
