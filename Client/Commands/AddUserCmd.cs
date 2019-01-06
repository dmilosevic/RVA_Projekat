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
    class AddUserCmd : BaseCommand
    {
        AddUserVM addUserVM = null;

        public AddUserCmd(AddUserVM vm)
        {
            addUserVM = vm;
        }
        

        public override void Execute(object parameter)
        {
            //if (parameter == null)
            //    return;

            //object[] parameters = parameter as object[];
            //if (parameters == null)
            //    return;

            //foreach (object o in parameters)
            //    if (o == null)
            //        return;

            if (addUserVM.Username == null || addUserVM.Username == "")
                return;
            if (addUserVM.Password == null || addUserVM.Password == "")
                return;
            if (addUserVM.FirstName == null || addUserVM.FirstName == "")
                return;
            if (addUserVM.LastName == null || addUserVM.LastName == "")
                return;

            User newUser = new User(addUserVM.Username, addUserVM.Password)
            {
                 FirstName = addUserVM.FirstName,
                 LastName = addUserVM.LastName,
                  isAdmin = addUserVM.IsAdmin,
            };

            bool success = UserProxy.Instance.Proxy.AddUser(newUser);

            if(success)
            {
                MessageBox.Show("User added", "Success");
                addUserVM.View.Close();
            }
            else
            {
                MessageBox.Show("User with this usrname already exists", "Failure");
            }
        }
    }
}
