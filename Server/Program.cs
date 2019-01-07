using Common.Model;
using Server.Context;
using Server.Services;
using Server.WCF_Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Projekat iz predmeta Razvoj Viseslojnih Aplikacija (u elektroenergetskim sistemima)
 * 
 * Projekat izradio: 
 * Danilo Milosevic
 * E3_112/2014
 * 
 * 
 * 
 * 7. Januara 2019.
 */
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine();
            Task.Factory.StartNew(() =>
            {
                InitializeData();
            });

            UserServer userServer = new UserServer();
            DataServer dataServer = new DataServer();
            userServer.OpenServer();
            dataServer.OpenServer();
            Console.WriteLine("Servers are up and running...\nPress enter to exit");
            //using (var context = new DataContext())
            //{
            //    var subs = new Substation() { Name = "dostanice" };

            //    context.Substations.Add(subs);

            //    context.Users.Add(new User("admin", "admin") { isAdmin = true, FirstName = "Danil", LastName = "Ishutin" });
            //    context.SaveChanges();
            //}


            Console.ReadLine();

            userServer.CloseServer();
            dataServer.CloseServer();
        }

        static void InitializeData()
        {
            using (var context = new DataContext())
            {
                #region Init admin user
                var admin = context.Users.FirstOrDefault(x => x.Username == "admin" && x.Password == "admin");
                if (admin == null)
                {
                    context.Users.Add(new User("admin", "admin") { FirstName = "Boza", LastName = "Adminic", isAdmin = true });
                    context.SaveChanges();
                }
                #endregion


                #region Init test data


                if (context.Substations.Count() <= 0)
                {
                    Substation s1 = new Substation() { Id=1, Name = "Plava kapa", Location = "Podlugovi" };
                    Substation s2 = new Substation() { Id=2, Name = "Plinara", Location = "Novi Becej" };

                    context.Substations.Add(s1);
                    context.Substations.Add(s2);
                    context.SaveChanges();
                }

                Random r = new Random();

                if(context.Devices.Count() <= 0)
                {
                    Substation[] substations = context.Substations.ToArray();
                    int subsNumber = substations.Length;
                   
                    Device d1 = new Device() { Id = "dev1", Name = "Kuvalo", Device_Substation = substations[r.Next(subsNumber)].Id };
                    Device d2 = new Device() { Id = "dev2", Name = "Kotao", Device_Substation = substations[r.Next(subsNumber)].Id };
                    Device d3 = new Device() { Id = "devic1", Name = "Pumpa", Device_Substation = substations[r.Next(subsNumber)].Id };
                    Device d4 = new Device() { Id = "devic2", Name = "Kompresor", Device_Substation = substations[r.Next(subsNumber)].Id };

                    context.Devices.Add(d1);
                    context.Devices.Add(d2);
                    context.Devices.Add(d3);
                    context.Devices.Add(d4);

                    context.SaveChanges();

                }

                if(context.Measurements.Count() <= 0)
                {
                    Device[] devices = context.Devices.ToArray();
                    int devNumber = devices.Length;

                    Measurement m1 = new Measurement()
                    { Id = 1, Type = "Local", Unit = "Volt", Value = 12.5F, DateTime=DateTime.Now, Measurement_Device = devices[r.Next(devNumber)].Id };

                    Measurement m2 = new Measurement()
                    { Id = 2, Type = "Global", Unit = "Ohm", Value = 3000F, DateTime=DateTime.Today.AddDays(-50), Measurement_Device = devices[r.Next(devNumber)].Id };

                    Measurement m3 = new Measurement()
                    { Id = 3, Type = "Suman", Unit = "Volt", Value = 3.5F, DateTime = DateTime.Today.AddDays(-120), Measurement_Device = devices[r.Next(devNumber)].Id };

                    Measurement m4 = new Measurement()
                    { Id = 4, Type = "Local", Unit = "Pascal", Value = 5000.35F, DateTime = DateTime.Today.AddDays(-300), Measurement_Device = devices[r.Next(devNumber)].Id };

                    context.Measurements.Add(m1);
                    context.Measurements.Add(m2);
                    context.Measurements.Add(m3);
                    context.Measurements.Add(m4);

                    context.SaveChanges();
                }
                #endregion
            }
        }
    }
}
