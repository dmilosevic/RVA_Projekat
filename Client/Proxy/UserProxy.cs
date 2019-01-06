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
        ClientCallback callbackInstance = new ClientCallback();
        
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
            
            DuplexChannelFactory<IUser> factory = new DuplexChannelFactory<IUser>(callbackInstance, binding, new EndpointAddress("net.tcp://localhost:40000/UserService"));
            Proxy = factory.CreateChannel();
        }

        

    }
}
