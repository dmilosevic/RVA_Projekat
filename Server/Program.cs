using Common.Model;
using Server.Context;
using Server.Services;
using Server.WCF_Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            UserServer server1 = new UserServer();
            DataServer server2 = new DataServer();
            server1.OpenServer();
            server2.OpenServer();
            //using (var context = new DataContext())
            //{
            //    var subs = new Substation() { Name = "dostanice" };

            //    context.Substations.Add(subs);

            //    context.Users.Add(new User("admin", "admin") { isAdmin = true, FirstName = "Danil", LastName = "Ishutin" });
            //    context.SaveChanges();
            //}


            Console.ReadLine();

            server1.CloseServer();
            server2.CloseServer();
        }
    }
}
