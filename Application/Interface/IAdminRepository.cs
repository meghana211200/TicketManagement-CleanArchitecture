using System;
using Domain.DTO;
using Domain.Entites;

namespace Application.Interface;

public interface IAdminRepository
{
    public Task<List<Ticket>> GetAllTickets();

    public Task<List<SupportEngineer>> GetAllAvailableSupportEng();

    public Task<List<Ticket>> GetTicketsFilter(string ticketFilter);

    public bool CheckTicket(int ticketId);

    public bool AddTicket(TicketTracker ticketTrackers);

    public bool UpdateTicket(int ticketId);

    public bool UpdateSupportEng(int seId);

   
}

