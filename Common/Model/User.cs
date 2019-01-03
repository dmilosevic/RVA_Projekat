using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool isAdmin { get; set; }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public User() { }
    }
}
