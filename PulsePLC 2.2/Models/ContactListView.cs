using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulsePLC_2._2.Models
{
    public class ContactListView
    {
        public string name { get; set; }
        public int count { get; set; }
        public int contactId { get; set; }

        public int devicesId { get; set; }
        public string serialNumber { get; set; }
    }
}
