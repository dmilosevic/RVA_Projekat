﻿using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.OpenDialogs
{
    public class OpenDataConflict : BaseCommand
    {
        public EditSubstationVM viewModel = null;

        public override void Execute(object parameter)
        {
            View.DataConflict window = new View.DataConflict(viewModel);
            window.ShowDialog();
        }

        public OpenDataConflict(EditSubstationVM home)
        {
            this.viewModel = home;
        }
    }
}
