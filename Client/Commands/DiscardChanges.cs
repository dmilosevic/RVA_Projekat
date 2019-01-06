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
    class DiscardChanges : BaseCommand
    {
        DataConflictVM conflictVM = null;

        public DiscardChanges(DataConflictVM vm)
        {
            conflictVM = vm;
        }

        public override void Execute(object parameter)
        {
            conflictVM.view.Close();
            conflictVM.editSubsVM.EditView.Close();
            conflictVM.editSubsVM.HomeVM.RefreshData();
        }
    }
}
