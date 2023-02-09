using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class TicketTracker
    {
        public int TicketTrackerId { get; set; }
        public int TicketId { get; set; }
        public int TicketTrackerSeId { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual SupportEngineer TicketTrackerSe { get; set; }
    }
}
