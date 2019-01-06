using Client.Commands;
using Client.Proxy;
using Client.ViewModel;
using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    class ClientCallback : IUserCallback
    {
        public void NotifyClientAboutChanges()
        {
            HomeVM.refreshCommand.Execute(null);
        }
    }
}
