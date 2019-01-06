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
    class LoginCommand : BaseCommand
    {
        LoginVM viewModel = null;

        public LoginCommand(LoginVM vm)
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

            string username = parameters[0].ToString();
            string password = parameters[1].ToString();

            User user = UserProxy.Instance.Proxy.Login(username, password);

            if(user == null)
            {
                MessageBox.Show("Invalid credentials", "Nope");
                return;
            }
            else
            {
                Home homeView = new Home(user);
                
                viewModel.view.Close();
                homeView.ShowDialog();
            }
        }
    }
}
