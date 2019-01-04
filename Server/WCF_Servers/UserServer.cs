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
    class UserServer
    {
        ServiceHost serviceHost;
        NetTcpBinding binding;
        string endPointAddress;

        public UserServer()
        {
            serviceHost = new ServiceHost(typeof(UserService));
            binding = new NetTcpBinding();
            endPointAddress = "net.tcp://localhost:40000/UserService";

            serviceHost.AddServiceEndpoint(typeof(IUser), binding, endPointAddress);
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
