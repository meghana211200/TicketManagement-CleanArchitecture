using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketTrackers = new HashSet<TicketTracker>();
        }

        public int TicketId { get; set; }
        public int TicketUserId { get; set; }
        public string Complaint { get; set; }
        public string TicketStatus { get; set; }

        public virtual User TicketUser { get; set; }
        public virtual ICollection<TicketTracker> TicketTrackers { get; set; }
    }
}
