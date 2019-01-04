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
    /// Singleton class for calling server on IUser
    /// </summary>
    class UserProxy
    {
        static UserProxy instance = null;

        public IUser Proxy { get; set; }

        public static UserProxy Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserProxy();

                return instance;
            }
        }

        private UserProxy()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IUser> factory = new ChannelFactory<IUser>(binding, new EndpointAddress("net.tcp://localhost:40000/UserService"));
            Proxy = factory.CreateChannel();
        }

        

    }
}
