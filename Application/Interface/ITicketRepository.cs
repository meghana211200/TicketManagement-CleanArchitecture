using System;
using Domain.Entites;

namespace Application.Interface;

public interface ITicketRepository
{

    public User CheckUser(int? id);

    public bool AddIssue(Ticket complaint);

    public Task<List<Ticket>> GetTickets(int? id);

    public bool UpdateTicket(Ticket ticket, int? ticketNo);

    public bool CheckTicket(int? ticketNo);

    public bool DeleteTicket(int? ticketNo);

}

