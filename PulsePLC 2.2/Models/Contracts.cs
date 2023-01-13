using System;
using System.Collections.Generic;

namespace PulsePLC_2._2
{
    public partial class Contracts
    {
        public Contracts()
        {
            Devices = new HashSet<Devices>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }
    }
}
