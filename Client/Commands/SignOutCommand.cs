using Client.Proxy;
using Client.View;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class SignOutCommand : BaseCommand
    {
        HomeVM homeVM = null;
        public SignOutCommand(HomeVM homeVM)
        {
            this.homeVM = homeVM;
        }

        public override void Execute(object parameter)
        {
            bool success = UserProxy.Instance.Proxy.SignOut(homeVM.CurrentUser.Username);
            
            Login window = new Login();
            window.Show();
            homeVM.View.Close();
        }
    }
}
