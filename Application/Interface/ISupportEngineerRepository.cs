using System;
using Domain.Entites;

namespace Application.Interface;

public interface ISupportEngineerRepository
{
    public TicketTracker CheckTicket(int ticket_id);

    public bool DeleteTicket(TicketTracker ticketTrackers);

    public bool UpdateTicket(int ticket_id);

    public bool UpdateSupportEng(int se_id);
}

