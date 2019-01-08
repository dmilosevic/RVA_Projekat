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
    class EditUserDataCmd : BaseCommand
    {
        EditUserDataVM editUserVM = null;
        Window view = null;

        public EditUserDataCmd(EditUserDataVM vm, Window view)
        {
            editUserVM = vm;
            this.view = view;
        }

        public override void Execute(object parameter)
        {
            if (editUserVM.Firstname == null || editUserVM.Firstname == "")
                return;
            if (editUserVM.Lastname == null || editUserVM.Lastname == "")
                return;

            string username = editUserVM.Username;
            string firstname = editUserVM.Firstname;
            string lastname = editUserVM.Lastname;
            User userToUpdate = new User()
            {
                Username = username,
                FirstName = firstname,
                LastName = lastname,
            };

            bool success = UserProxy.Instance.Proxy.UpdateUserInfo(userToUpdate);

            if(success)
            {
                LoginVM.Log.Info($"User updated. Username = ('{userToUpdate.Username}')");

                editUserVM.View.Close();
                editUserVM.homeVM.CurrentUser = new User()
                {
                     Id = editUserVM.homeVM.CurrentUser.Id,
                      Username = editUserVM.homeVM.CurrentUser.Username,
                      isAdmin = editUserVM.homeVM.CurrentUser.isAdmin,
                       Password = editUserVM.homeVM.CurrentUser.Password,
                        FirstName = firstname,
                        LastName = lastname,
                };
               // editUserVM.homeVM.CurrentUser.FirstName = firstname;
                //editUserVM.homeVM.CurrentUser.LastName = lastname;
            }
            else
            {
                LoginVM.Log.Error($"Could not update user info. Username = ('{userToUpdate.Username}')");
            }
        }
    }
}
