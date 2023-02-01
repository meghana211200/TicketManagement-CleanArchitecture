using System;
using Application.Interface;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SupportEngineerRepository : ISupportEngineerRepository
{
    private readonly TicketManagementDbContext _context;

    public SupportEngineerRepository(TicketManagementDbContext context)
    {
        _context = context;
    }

    public  TicketTracker CheckTicket(int ticket_id)
    {
        TicketTracker checkTicket =  _context.TicketTracker.Where(e => e.ticket_id == ticket_id).SingleOrDefault();
        return checkTicket;
    }
    public bool DeleteTicket(TicketTracker ticketTrackers)
    {
        _context.TicketTracker.Remove(ticketTrackers);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateTicket(int ticket_id)
    {
        var updateTicket = _context.Tickets.Where(x => x.ticket_id == ticket_id).SingleOrDefault();
        if (updateTicket != null)
        {
            updateTicket.ticket_status = "Resolved";
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateSupportEng(int se_id)
    {
        var supportEngineer = _context.SupportEngineer.Where(x => x.se_id == se_id).SingleOrDefault();
        if (supportEngineer != null)
        {
            supportEngineer.isAvailable = true;
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }
}

