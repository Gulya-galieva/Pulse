using System;
using System.Collections.Generic;

namespace PulsePLC_2._2
{
    public partial class Actions
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public int ActionTypeId { get; set; }

        public virtual ActionTypes ActionType { get; set; }
        public virtual Devices Device { get; set; }
        public virtual Users User { get; set; }
    }
}
