using Common.Contracts;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.WCF_Servers
{
    class DataServer
    {
        ServiceHost serviceHost;
        NetTcpBinding binding;
        string endPointAddress;

        public DataServer()
        {
            serviceHost = new ServiceHost(typeof(DataService));
            binding = new NetTcpBinding();
            endPointAddress = "net.tcp://localhost:50000/DataService";

            serviceHost.AddServiceEndpoint(typeof(IData), binding, endPointAddress);
        }

        public void OpenServer()
        {
            serviceHost.Open();
        }

        public void CloseServer()
        {
            if (serviceHost.State != CommunicationState.Closed)
                serviceHost.Close();
        }
    }
}
