using System;
using Domain.DTO;
using Domain.Entites;

namespace Application.Services.Admin;

public interface IAdminService
{
    public Task<List<Tickets>> GetAllTicket();

    public Task<List<SupportEngineer>> GetAllAvailableSupportEng();

    public Task<List<Tickets>> GetTicketsFilter(TicketFilterDTO ticketFilter);

    public string AssignTicket(TicketTrackerDTO ticketTracker);
}


