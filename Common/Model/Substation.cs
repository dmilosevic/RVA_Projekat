using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Substation : SubstationPrototype
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }

        public List<Device> Devices { get; set; }

        public Substation()
        {
            Devices = new List<Device>();

        }

        public override string ToString()
        {
            return string.Format("{0}_{1} ({2})", Id, Name, Location);
        }

        public override bool Equals(object obj)
        {
            Substation otherSub = obj as Substation;
            if (otherSub == null)
                return false;

            return this.Id       == otherSub.Id     &&
                   this.Name     == otherSub.Name   &&
                   this.Location == otherSub.Location;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Substation Clone()
        {
            Substation clone = new Substation();

            clone.Location = this.Location;
            clone.Name = this.Name;
            clone.Devices = this.Devices;

            return clone;
        }
    }
}
