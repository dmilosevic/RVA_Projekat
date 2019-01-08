using Common.Contracts;
using Common.Model;
using Server.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class UserService : IUser
    {
        static readonly object dummyObj = new object();
        IUserCallback callback = null;

        public UserService()
        {
            
        }

        public bool AddUser(User newUser)
        {
            lock(dummyObj)
            {
                using (var context = new DataContext())
                {
                    var existingUser = context.Users.FirstOrDefault(u => u.Username == newUser.Username); //returns null if no match
                    if(existingUser != null)
                    {
                        //user with this name already exists
                        Program.Log.Error($"Existing user. Username=('{existingUser.Username}')");
                        return false;
                    }
                    context.Users.Add(newUser);
                    context.SaveChanges();                   
                }
            }
            Program.Log.Info($"New user added Username=('{newUser.Username}')");
            return true;
        }

        public User Login(string username, string password)
        {
            lock (dummyObj) //jel neophodan lock ovde? Radim nad istim podacima, ali svaki put sa novom instancom DataContext-a i ne modifikujem podatke ovde, samo citam.
            {
                using (var context = new DataContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password); //returns null if no match

                    if (user != null)
                    {
                        try
                        {
                            callback = OperationContext.Current.GetCallbackChannel<IUserCallback>();
                            CallbackData.Users.Add(user.Username, callback);
                            //CallbackData.Callbacks.Add(callback);
                        }
                        catch(Exception)
                        {
                            Program.Log.Warn($"Already logged in. Username=('{username}')");
                            return null;
                        }
                        
                    }

                    Program.Log.Info($"Successful Login. Username=('{username}')");
                    return user;
                }
            }
        }

        public bool SignOut(string username)
        {
            if (CallbackData.Users.ContainsKey(username))
            {
                CallbackData.Users.Remove(username);
                Program.Log.Info($"User Signed out. Username=('{username}')");
                return true;
            }
            else
                return false;           
        }

        public bool UpdateUserInfo(User user)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var userFromDB = context.Users.FirstOrDefault(u => u.Username == user.Username); //returns null if no match
                    if (userFromDB == null)
                    {
                        //non-existing user
                        Program.Log.Error($"Non-existing user. Username=('{user.Username}')");
                        return false;
                    }

                    //update properties
                    userFromDB.FirstName = user.FirstName;
                    userFromDB.LastName = user.LastName;

                    context.SaveChanges();
                }
            }
            Program.Log.Error($"Succesfully updated user. Username=('{user.Username}')");
            return true;
        }
    }
}
