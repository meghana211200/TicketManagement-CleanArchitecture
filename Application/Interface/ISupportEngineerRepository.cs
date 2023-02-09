using System;
using Domain.Entites;

namespace Application.Interface;

public interface ISupportEngineerRepository
{
    public TicketTracker CheckTicket(int ticketId);

    public bool DeleteTicket(TicketTracker ticketTrackers);

    public bool UpdateTicket(int ticketId);

    public bool UpdateSupportEng(int seId);
}

