using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class CallbackData
    {
        public static Dictionary<string, IUserCallback> Users = new Dictionary<string, IUserCallback>();

        public static List<IUserCallback> Callbacks = new List<IUserCallback>();
    }
}
