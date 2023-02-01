using System;
using Domain.DTO;

namespace Application.Services.SupportEngineers;

public interface ISupportEngineerService
{
    public string CloseTicket(TicketTrackerDTO ticketTracker);
}
