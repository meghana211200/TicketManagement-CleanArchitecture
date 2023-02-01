using System;
using Domain.DTO;
using Domain.Entites;

namespace Application.Interface;

public interface IAdminRepository
{
    public Task<List<Tickets>> GetAllTickets();

    public Task<List<SupportEngineer>> GetAllAvailableSupportEng();

    public Task<List<Tickets>> GetTicketsFilter(string ticketFilter);

    public bool CheckTicket(int ticket_id);

    public bool AddTicket(TicketTracker ticketTrackers);

    public bool UpdateTicket(int ticket_id);

    public bool UpdateSupportEng(int se_id);

   
}

