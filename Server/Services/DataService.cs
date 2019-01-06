using Common.Contracts;
using Common.Model;
using Server.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class DataService : IData
    {
        static readonly object dummyObj = new object();

        public bool AddDevice(Device device)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var existingDevice = context.Devices.FirstOrDefault(x =>  x.Id == device.Id &&
                            //x.Device_Substation.Id == device.Device_Substation.Id && 
                            x.Name == device.Name); //returns null if no match

                    if (existingDevice != null)
                    {
                        //device with this name already exists at the same substation
                        return false;
                    }
                    context.Devices.Add(device);
                    context.SaveChanges();
                }
            }
            return true;
        }

        public bool AddMeasurement(Measurement meas)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var existingMeasurement = context.Measurements.FirstOrDefault(x => x.Id == meas.Id &&
                            //x.Measurement_Device.Id == meas.Measurement_Device.Id &&
                            x.DateTime.ToLongTimeString() == meas.DateTime.ToLongTimeString()); //returns null if no match

                    if (existingMeasurement != null)
                    {
                        //measurement with this time already exists for this device
                        return false;
                    }
                    context.Measurements.Add(meas);
                    context.SaveChanges();
                }
            }
            return true;
        }

        public bool AddSubstation(Substation sub/*, string user*/)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    //mozda ni ne treba provera za duplikate, jer zadatak zahteva implementaciju komande koja "duplira primarne podatke" stoga ce tad biti podudaranja.
                    var existingStation = context.Substations.FirstOrDefault(x => x.Location == sub.Location && x.Name == sub.Name); //returns null if no match

                    
                    if (existingStation != null)
                    {
                        //substation with this name & location already exists
                        return false;
                    }
                    context.Substations.Add(sub);
                    context.SaveChanges();
                    Task.Factory.StartNew(() =>
                    {
                        NotifyUsersAboutChange();
                    });
                }
            }
            return true;
        }

        private void NotifyUsersAboutChange(/*string currentUser*/)
        {
            //foreach(KeyValuePair<string, IUserCallback> pair in CallbackData.Users)
            //{
            //    if (pair.Key != currentUser)
            //        pair.Value.NotifyClientAboutChanges();
            //}

            foreach(var cb in CallbackData.Callbacks)
            {
                cb.NotifyClientAboutChanges();
            }
        }

        public bool DeleteDevice(string id)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var dev = context.Devices.FirstOrDefault(x => x.Id == id);
                    if (dev == null)
                        return false;

                    context.Devices.Remove(dev);
                    context.SaveChanges();
                }
            }
            return true;
        }

        public bool DeleteMeasurement(int id)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var measurement = context.Measurements.FirstOrDefault(x => x.Id == id);
                    if (measurement == null)
                        return false;                    

                    context.Measurements.Remove(measurement);
                    context.SaveChanges();
                }
            }
            return true;
        }

        public bool DeleteSubstation(int id)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var station = context.Substations.FirstOrDefault(x => x.Id == id);
                    if (station == null)
                        return false;

                    //first delete containing devices
                    var devicesInThisSubstation = GetDevices(new Substation() { Id = id });
                    foreach (var device in devicesInThisSubstation)
                    {
                        foreach (var measure in GetMeasurements(device))
                        {
                            context.Measurements.Attach(measure);
                            context.Measurements.Remove(measure);
                            context.SaveChanges();
                            
                        }

                        context.Devices.Attach(device);
                        context.Devices.Remove(device);
                        context.SaveChanges();
                    }


                    context.Substations.Remove(station);
                    context.SaveChanges();
                    Task.Factory.StartNew(() =>
                    {
                        NotifyUsersAboutChange();
                    });
                }
            }
            return true;
        }

        public List<Substation> GetAllSubstations()
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var substations = context.Substations;
                    return substations.ToList();
                }
            }
        }

        public List<Device> GetDevices(Substation substation)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var devices = context.Devices.Where(x => x.Device_Substation.Id == substation.Id); //it should work xD

                    return devices.ToList();
                }
            }
        }

        public List<Measurement> GetMeasurements(Device device)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var measurements = context.Measurements.Where(x => x.Measurement_Device.Id == device.Id);
                    return measurements.ToList();
                }
            }
        }

        public Substation GetSubstationById(int id)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var substation = context.Substations.FirstOrDefault(x => x.Id == id);
                    return substation;
                }
            }
        }

        public bool UpdateDevice(Device device)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var deviceFromDB = context.Devices.FirstOrDefault(x => x.Id == device.Id);
                    if (deviceFromDB == null)
                        return false;

                    deviceFromDB.Device_Substation = device.Device_Substation;
                    deviceFromDB.Name = device.Name;                    

                    context.SaveChanges();
                }
            }
            return true;
        }

        public bool UpdateMeasurement(Measurement meas)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var measFromDB = context.Measurements.FirstOrDefault(x => x.Id == meas.Id);
                    if (measFromDB == null)
                        return false;

                    measFromDB.Measurement_Device = meas.Measurement_Device;
                    measFromDB.DateTime = meas.DateTime;
                    measFromDB.Type = meas.Type;
                    measFromDB.Unit = meas.Unit;
                    measFromDB.Value = meas.Value; //I could do some cloning here.... there goes a pattern..

                    context.SaveChanges();
                }
            }
            return true;
        }

        public bool UpdateSubstation(Substation sub)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var subFromDB = context.Substations.FirstOrDefault(x => x.Id == sub.Id);
                    if (subFromDB == null)
                        return false;

                    subFromDB.Location = sub.Location;
                    subFromDB.Name = sub.Name;

                    context.SaveChanges();
                    Task.Factory.StartNew(() =>
                    {
                        NotifyUsersAboutChange();
                    });
                }
            }
            return true;
        }
    }
}
