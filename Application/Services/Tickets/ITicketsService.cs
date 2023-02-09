using System;
using Domain.Entites;

namespace Application.Services.Tickets;

public interface ITicketsService
{
    public string CreateTickets(Ticket ticket, int id);

    public Task<List<Ticket>> GetTicket(int? id);

    public string UpdateTickets(Ticket ticket, int? ticketNo);

    public string DeleteTicket(int ticketNo);
}

