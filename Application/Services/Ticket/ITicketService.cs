using System;
using Domain.Entites;

namespace Application.Services.Ticket;

public interface ITicketService
{
    public string CreateTicket(Tickets ticket, int? id);

    public Task<List<Tickets>> GetTicket(int? id);

    public string UpdateTicket(Tickets ticket, int? ticketNo);

    public string DeleteTicket(int? ticketNo);
}


