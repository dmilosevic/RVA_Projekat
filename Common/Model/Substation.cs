﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Substation
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }

        List<Device> Devices { get; set; }
    }
}
