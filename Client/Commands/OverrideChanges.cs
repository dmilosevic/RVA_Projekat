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
                MessageBox.Show("Substation updated.", "Success");
                conflictVM.view.Close();
                conflictVM.editSubsVM.EditView.Close();
                conflictVM.editSubsVM.HomeVM.RefreshData();
            }
            else
            {
                MessageBox.Show("Substation could not be updated", "Failure");
            }
        }
    }
}
