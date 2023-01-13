using System;
using System.Collections.Generic;

namespace PulsePLC_2._2
{
    public partial class Devices
    {
        public Devices()
        {
            Actions = new HashSet<Actions>();
        }

        public int Id { get; set; }
        public string Serial { get; set; }
        public int? ContractId { get; set; }

        public virtual Contracts Contract { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
    }
}
