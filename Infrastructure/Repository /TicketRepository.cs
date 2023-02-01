using System;
using Application.Interface;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TicketRepository : ITicketRepository
{

    private readonly TicketManagementDbContext _context;

    public TicketRepository(TicketManagementDbContext context)
    {
        _context = context;
    }

    public User CheckUser(int? id)
    {
        var user = _context.User.Where(x => x.user_id == id).SingleOrDefault();
        return user;

    }

    public bool AddIssue(Tickets complaint)
    {
        _context.Tickets.Add(complaint);
        _context.SaveChanges();
        return true;

    }

    public async Task<List<Tickets>> GetTickets(int? id)
    {
        List<Tickets> ticket = await _context.Tickets.Where(x => x.ticket_user_id == id).ToListAsync();
        return (ticket);
    }

    public bool UpdateTicket(Tickets ticket, int? ticketNo)
    {
        var updateTicket = _context.Tickets.Where(x => x.ticket_id == ticketNo).SingleOrDefault();
        updateTicket.complaint = ticket.complaint;
        _context.SaveChanges();
        return true;
    }

    public bool CheckTicket(int? ticketNo)
    {
        var ticket = _context.Tickets.Where(e => e.ticket_id == ticketNo).SingleOrDefault();
        if (ticket == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool DeleteTicket(int? ticketNo)
    {
        var ticket = _context.Tickets.Where(e => e.ticket_id == ticketNo).SingleOrDefault();
        _context.Tickets.Remove(ticket);
        _context.SaveChanges();
        return true;
    }
}

