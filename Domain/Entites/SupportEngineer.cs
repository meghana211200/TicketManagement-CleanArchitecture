using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class SupportEngineer
    {
        public SupportEngineer()
        {
            TicketTrackers = new HashSet<TicketTracker>();
        }

        public int SeId { get; set; }
        public int SeUserId { get; set; }
        public Boolean IsAvailable { get; set; }

        public virtual User SeUser { get; set; }
        public virtual ICollection<TicketTracker> TicketTrackers { get; set; }
    }
}
