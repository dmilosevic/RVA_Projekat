using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Proxy
{
    /// <summary>
    /// Singleton class for calling server on IData
    /// </summary>
    class DataProxy
    {
        static DataProxy instance = null;

        public IData Proxy { get; set; }

        public static DataProxy Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProxy();

                return instance;
            }
        }

        private DataProxy()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IData> factory = new ChannelFactory<IData>(binding, new EndpointAddress("net.tcp://localhost:50000/DataService"));
            Proxy = factory.CreateChannel();
        }
    }
}
