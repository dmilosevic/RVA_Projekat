using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    [ServiceContract]
    public interface IData
    {
        #region CREATE operations

        [OperationContract]
        bool AddSubstation(Substation sub);

        [OperationContract]
        bool AddDevice(Device device);

        [OperationContract]
        bool AddMeasurement(Measurement meas);

        #endregion

        #region READ operations

        [OperationContract]
        List<Substation> GetAllSubstations();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Matching substation or null</returns>
        [OperationContract]        
        Substation GetSubstationById(int id);

        [OperationContract]
        /// <summary>
        /// Gets all devices of a substation
        /// </summary>
        /// <param name="substation">substation which all returned devices link to</param>
        /// <returns></returns>
        List<Device> GetDevices(Substation substation);

       
        [OperationContract]
        /// <summary>
        /// Gets all measurements for given device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        List<Measurement> GetMeasurements(Device device);
        #endregion
        

        #region UPDATE operations

        [OperationContract]
        bool UpdateSubstation(Substation sub);

        [OperationContract]
        bool UpdateDevice(Device device);

        [OperationContract]
        bool UpdateMeasurement(Measurement meas);

        #endregion

        #region DELETE operations

        [OperationContract]
        bool DeleteSubstation(int id);

        [OperationContract]
        bool DeleteDevice(string id);

        [OperationContract]
        bool DeleteMeasurement(int  id);

        #endregion

    }
}
