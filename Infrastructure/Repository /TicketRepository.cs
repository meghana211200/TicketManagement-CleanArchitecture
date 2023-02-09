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
        var user = _context.Users.Where(x => x.UserId== id).SingleOrDefault();
        return user;

    }

    public bool AddIssue(Ticket complaint)
    {
        _context.Tickets.Add(complaint);
        _context.SaveChanges();
        return true;

    }

    public async Task<List<Ticket>> GetTickets(int? id)
    {
        List<Ticket> ticket = await _context.Tickets.Where(x => x.TicketUserId== id).ToListAsync();
        return (ticket);
    }

    public bool UpdateTicket(Ticket ticket, int? ticketNo)
    {
        var updateTicket = _context.Tickets.Where(x => x.TicketId == ticketNo).SingleOrDefault();
        updateTicket.Complaint = ticket.Complaint;
        _context.SaveChanges();
        return true;
    }

    public bool CheckTicket(int? ticketNo)
    {
        var ticket = _context.Tickets.Where(e => e.TicketId == ticketNo).SingleOrDefault();
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
        var ticket = _context.Tickets.Where(e => e.TicketId == ticketNo).SingleOrDefault();
        _context.Tickets.Remove(ticket);
        _context.SaveChanges();
        return true;
    }
}

