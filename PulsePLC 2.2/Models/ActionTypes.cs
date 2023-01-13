using System;
using System.Collections.Generic;

namespace PulsePLC_2._2
{
    public partial class ActionTypes
    {
        public ActionTypes()
        {
            Actions = new HashSet<Actions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Actions> Actions { get; set; }
    }
}
