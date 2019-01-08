using Client.Proxy;
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
    class OverrideChanges : BaseCommand
    {
        DataConflictVM conflictVM = null;

        public OverrideChanges(DataConflictVM vm)
        {
            conflictVM = vm;
        }

        public override void Execute(object parameter)
        {
            Substation subs = new Substation()
            {
                Id = conflictVM.editSubsVM.Id,
                Name = conflictVM.editSubsVM.Name,
                Location = conflictVM.editSubsVM.Location,
            };

            bool success = DataProxy.Instance.Proxy.UpdateSubstation(subs);

            if (success)
            {
                LoginVM.Log.Info("Conflict arrised. Changes Overriden");
                LoginVM.Log.Info($"Substation updated. Id=('{subs.Id}')");

                conflictVM.view.Close();
                conflictVM.editSubsVM.EditView.Close();
                conflictVM.editSubsVM.HomeVM.RefreshData();
            }
            else
            {
                LoginVM.Log.Error($"Substation could not be updated. Id=('{subs.Id}')");
            }
        }
    }
}
