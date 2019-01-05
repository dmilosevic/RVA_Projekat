using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public float Value { get; set; }

        public DateTime DateTime { get; set; }

        public Device Measurement_Device { get; set; }

        public Measurement()
        {

        }

        public override string ToString()
        {
            return string.Format("{0}_{1} = {2} [{3}]", Id, Type, Value, Unit);
        }
    }
}
