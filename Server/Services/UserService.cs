﻿using Common.Contracts;
using Common.Model;
using Server.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class UserService : IUser
    {
        static readonly object dummyObj = new object();

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
                        return false;
                    }
                    context.Users.Add(newUser);
                    context.SaveChanges();                   
                }
            }
            return true;
        }

        public User Login(string username, string password)
        {
            lock (dummyObj) //jel neophodan lock ovde? Radim nad istim podacima, ali svaki put sa novom instancom DataContext-a i ne modifikujem podatke ovde, samo citam.
            {
                using (var context = new DataContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password); //returns null if no match
                    return user;
                }
            }
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
                        return false;
                    }

                    //update properties
                    userFromDB.FirstName = user.FirstName;
                    userFromDB.LastName = user.LastName;

                    context.SaveChanges();
                }
            }
            return true;
        }
    }
}