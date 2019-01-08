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
    class AddSubstation : BaseCommand
    {
        AddSubstationVM viewModel = null;

        public AddSubstation(AddSubstationVM vm)
        {
            viewModel = vm;
        }


        public override void Execute(object parameter)
        {
            #region input validation
            if (parameter == null ||
                !(parameter is Object[]))
                return;

            Object[] parameters = parameter as Object[];
            if (parameters == null || parameters.Length != 2)
                return;

            foreach (var v in parameters)
            {
                if (v == null || v.ToString().Length == 0)
                    return;
            }
            #endregion

            string name = parameters[0].ToString();
            string location = parameters[1].ToString();

            Substation newSub = new Substation() { Name = name, Location = location };

            bool success = DataProxy.Instance.Proxy.AddSubstation(newSub/*, viewModel.homeVM.CurrentUser.Username*/);

            if(!success)
            {
                LoginVM.Log.Error($"Tried to add duplicate Substation. Id=('{newSub.Id}')");
                return;
            }
            else
            {
                LoginVM.Log.Info($"Substation added successfuly. Id=('{newSub.Id}')");

                //save for undo
                viewModel.homeVM.UndoHistory.Add(this);
                viewModel.homeVM.SubstationsUndo.Add(newSub);


                viewModel.homeVM.RefreshData();
                viewModel.view.Close();
            }            
        }

        public override void UnExecute()
        {
            // Delete
            var listaUndo = viewModel.homeVM.SubstationsUndo;
            Substation subs = listaUndo[listaUndo.Count - 1]; //get last item

            DataProxy.Instance.Proxy.DeleteSubstation(subs.Id);
            viewModel.homeVM.SubstationsUndo.RemoveAt(viewModel.homeVM.SubstationsUndo.Count - 1);
            viewModel.homeVM.RefreshData();

            LoginVM.Log.Info($"UNDO command - Substation deleted. Id=('{subs.Id}')");

            viewModel.homeVM.RedoHistory.Add(this);
            viewModel.homeVM.SubstationsRedo.Add(subs);
        }
       

        
    }
}
