using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Domain.Entites;

public class TicketTracker
{

    [Key]
    public int ticketTracker_id { get; set; }
    public int ticket_id { get; set; }

    public int ticketTracker_se_id { get; set; }

    [ForeignKey("ticket_id")]
    public virtual Tickets ticket { get; set; }

    [ForeignKey("ticketTracker_se_id")]
    public virtual SupportEngineer supportEngineer { get; set; }

}
