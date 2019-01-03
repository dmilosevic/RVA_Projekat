using Common.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dbConnection2015")
        {
            Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory()); //fixes bug when evaluating |DataDirectory| in connection string
        }

        public DbSet<Substation> Substations { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements { get; set; }


        public DbSet<User> Users { get; set; }
    }
}
