using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    [ServiceContract(CallbackContract = typeof(IUserCallback), SessionMode = SessionMode.Required)]
    public interface IUser
    {
        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool UpdateUserInfo(User user);

    }


    /// <summary>
    /// Provides server with option to notify clients about data changes made by other users
    /// </summary>
    public interface IUserCallback
    {
        [OperationContract]
        void NotifyClientAboutChanges();
    }
}
