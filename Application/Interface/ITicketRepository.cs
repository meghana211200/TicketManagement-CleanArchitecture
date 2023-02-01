using System;
using Domain.Entites;

namespace Application.Interface;

public interface ITicketRepository
{

    public User CheckUser(int? id);

    public bool AddIssue(Tickets complaint);

    public Task<List<Tickets>> GetTickets(int? id);

    public bool UpdateTicket(Tickets ticket, int? ticketNo);

    public bool CheckTicket(int? ticketNo);

    public bool DeleteTicket(int? ticketNo);

}

