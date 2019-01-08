using Client.Proxy;
using Client.View;
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
    public class EditSubstation : BaseCommand
    {
        EditSubstationVM viewModel = null;

        public EditSubstation(EditSubstationVM vm)
        {
            viewModel = vm;
        }

        public override void Execute(object parameter)
        {
            #region validation check
            if (parameter == null)
                return;

            object[] parameters = parameter as object[];

            if (parameters == null)
                return;

            if (parameters[1].ToString() == "" || parameters[2].ToString() == "")
                return;
            #endregion

            Substation updatedSubs = new Substation()
            {
                Id = Convert.ToInt32(parameters[0]),
                Name = parameters[1].ToString(),
                Location = parameters[2].ToString(),
            };

            Substation storedSubs = DataProxy.Instance.Proxy.GetSubstationById(viewModel.oldSubstation.Id);
            if(storedSubs == null)
            {
                MessageBox.Show("This substation was deleted in the meantime.", "Error");
                viewModel.EditView.Close();
                viewModel.HomeVM.RefreshData();
                return;
            }

            if(!storedSubs.Equals(viewModel.oldSubstation))
            {
                //neko je menjao podatke u medjuvremenu -> TAKE ACTION

                DataConflict window = new DataConflict(viewModel);
                window.ShowDialog();
                
            }
            else 
            {
                //podaci nisu menjani u medjuvremenu -> UPDATE

                bool success = DataProxy.Instance.Proxy.UpdateSubstation(updatedSubs);

                if (success)
                {
                    LoginVM.Log.Info($"Substation updated. Id = ('{updatedSubs.Id}')");
                    viewModel.EditView.Close();
                    viewModel.HomeVM.RefreshData();
                }
                else
                {
                    LoginVM.Log.Error($"Substation could not be updated. Id = ('{updatedSubs.Id}')");
                }
            }

            
            
        }
    }
}
