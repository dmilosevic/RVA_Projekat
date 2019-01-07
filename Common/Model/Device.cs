using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }

        //public Substation Device_Substation { get; set; }
        public int Device_Substation { get; set; }
        public List<Measurement> Measurements { get; set; }


        public Device()
        {
            Measurements = new List<Measurement>();
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", Id, Name);
        }
    }
}
