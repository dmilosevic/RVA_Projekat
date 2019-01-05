using Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel
{
    class AddSubstationVM
    {
        public Window view = null;
        public HomeVM homeVM = null;

        public AddSubstation addSubstationCmd { get; set; }

        public AddSubstationVM(Window view, HomeVM home)
        {
            this.view = view;
            this.homeVM = home;
            addSubstationCmd = new AddSubstation(this);
        }


    }
}
