using System;
using System.Collections.Generic;

namespace PulsePLC_2._2
{
    public partial class Users
    {
        public Users()
        {
            Actions = new HashSet<Actions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public bool AccessAddFromAssembly { get; set; }
        public bool AccessSend { get; set; }
        public bool AccessGetToRepair { get; set; }

        public virtual ICollection<Actions> Actions { get; set; }
    }
}
