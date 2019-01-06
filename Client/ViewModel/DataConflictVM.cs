using Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    class DataConflictVM
    {
        public EditSubstationVM editSubsVM = null;
        public Window view = null;

        public OverrideChanges overrideChangesCmd { get; set; }
        public DiscardChanges discardChangesCmd { get; set; }

        public DataConflictVM(EditSubstationVM vm, Window view)
        {
            editSubsVM = vm;
            this.view = view;

            overrideChangesCmd = new OverrideChanges(this);
            discardChangesCmd = new DiscardChanges(this);
        }
    }
}
