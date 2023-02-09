using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class User
    {
        public User()
        {
            SupportEngineers = new HashSet<SupportEngineer>();
            Tickets = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }

        public virtual ICollection<SupportEngineer> SupportEngineers { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
