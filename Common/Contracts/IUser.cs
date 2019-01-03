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
    public interface IUser
    {
        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool UpdateUserInfo(User user);

    }
}
