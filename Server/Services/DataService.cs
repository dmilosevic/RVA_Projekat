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
                    var existingDevice = context.Devices.FirstOrDefault(x => x.Id == device.Id); //returns null if no match

                    if (existingDevice != null)
                    {
                        Program.Log.Error($"Tried to add device with existing Id (ID='{device.Id}')");
                        return false;
                    }
                    context.Devices.Add(device);
                    context.SaveChanges();
                }
            }
            Program.Log.Info($"New device has been added (ID = '{device.Id}')");
            return true;
        }

        public bool AddMeasurement(Measurement meas)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var existingMeasurement = context.Measurements.FirstOrDefault(x => x.Id == meas.Id);

                    if (existingMeasurement != null)
                    {
                        Program.Log.Error($"Tried to add measurement with existing Id (ID='{meas.Id}')");
                        return false;
                    }
                    context.Measurements.Add(meas);
                    context.SaveChanges();
                }
            }
            Program.Log.Info($"New measurement has been added (ID = '{meas.Id}')");
            return true;
        }

        public bool AddSubstation(Substation sub/*, string user*/)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {                    
                    var existingStation = context.Substations.FirstOrDefault(x => x.Id == sub.Id);
                    
                    if (existingStation != null)
                    {
                        Program.Log.Error($"Tried to add substation with existing Id (ID='{sub.Id}')");
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
            Program.Log.Info($"New substation has been added (ID = '{sub.Id}')");
            return true;
        }

        private void NotifyUsersAboutChange(/*string currentUser*/)
        {
            foreach (KeyValuePair<string, IUserCallback> pair in CallbackData.Users)
            {
                try
                {
                    //if (pair.Key != currentUser)
                    pair.Value.NotifyClientAboutChanges();
                }
                catch (Exception) { }                
            }

            //foreach(var cb in CallbackData.Callbacks)
            //{
            //    cb.NotifyClientAboutChanges();
            //}
        }

        public bool DeleteDevice(string id)
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var dev = context.Devices.FirstOrDefault(x => x.Id == id);
                    if (dev == null)
                    {
                        Program.Log.Error($"Tried to delete non-existing device (ID='{id}')");
                        return false;
                    }

                    context.Devices.Remove(dev);
                    context.SaveChanges();
                }
            }
            Program.Log.Info($"Device has been deleted (ID = '{id}')");
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
                    {
                        Program.Log.Error($"Tried to delete non-existing measurement (ID='{id}')");
                        return false;
                    }

                    context.Measurements.Remove(measurement);
                    context.SaveChanges();
                }
            }
            Program.Log.Info($"Measurement has been deleted (ID = '{id}')");
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
                    {
                        Program.Log.Error($"Tried to delete non-existing substation (ID='{id}')");
                        return false;
                    }
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
            Program.Log.Info($"Substation has been deleted (ID = '{id}')");
            return true;
        }

        public List<Substation> GetAllSubstations()
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var substations = context.Substations;

                    Program.Log.Info("All substations has been requested and returned");

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
                    var devices = context.Devices.Where(x => x.Device_Substation == substation.Id);
                    Program.Log.Info($"All devices in substation have been requested and returned. Substation_ID = ('{substation.Id}')");
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
                    var measurements = context.Measurements.Where(x => x.Measurement_Device == device.Id);
                    Program.Log.Info($"All measurements in device have been requested and returned. Device_ID = ('{device.Id}')");
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
                    Program.Log.Info($"Substation was returned. Substation_ID = ('{id}')");
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
                    {
                        Program.Log.Error($"Update to non-existing device. ID = ('{device.Id}')");
                        return false;
                    }

                    deviceFromDB.Device_Substation = device.Device_Substation;
                    deviceFromDB.Name = device.Name;                    

                    context.SaveChanges();
                }
            }
            Program.Log.Info($"Device was updated. ID = ('{device.Id}')");
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
                    {
                        Program.Log.Error($"Update to non-existing measurement. ID = ('{meas.Id}')");
                        return false;
                    }

                    measFromDB.Measurement_Device = meas.Measurement_Device;
                    measFromDB.DateTime = meas.DateTime;
                    measFromDB.Type = meas.Type;
                    measFromDB.Unit = meas.Unit;
                    measFromDB.Value = meas.Value; //I could do some cloning here.... there goes a pattern..

                    context.SaveChanges();
                }
            }
            Program.Log.Info($"Measurement was updated. ID = ('{meas.Id}')");
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
                    {
                        Program.Log.Error($"Update to non-existing substation. ID = ('{sub.Id}')");
                        return false;
                    }
                    subFromDB.Location = sub.Location;
                    subFromDB.Name = sub.Name;

                    context.SaveChanges();
                    Task.Factory.StartNew(() =>
                    {
                        NotifyUsersAboutChange();
                    });
                }
            }
            Program.Log.Info($"Substation was updated. ID = ('{sub.Id}')");
            return true;
        }

        public List<Device> GetAllDevices()
        {
            lock (dummyObj)
            {
                using (var context = new DataContext())
                {
                    var devices = context.Devices;

                    Program.Log.Info("All devices has been requested and returned");

                    return devices.ToList();
                }
            }
        }
    }
}
