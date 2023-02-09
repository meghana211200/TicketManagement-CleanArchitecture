using System;
using Domain.DTO;
using Domain.Entites;

namespace Application.Services.Admin;

public interface IAdminService
{
    public Task<List<Ticket>> GetAllTicket();

    public Task<List<SupportEngineer>> GetAllAvailableSupportEng();

    public Task<List<Ticket>> GetTicketsFilter(TicketFilterDTO ticketFilter);

    public string AssignTicket(TicketTrackerDTO ticketTracker);
}


