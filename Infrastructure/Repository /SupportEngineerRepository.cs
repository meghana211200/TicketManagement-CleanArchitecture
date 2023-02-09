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

    public  TicketTracker CheckTicket(int ticketId)
    {
        TicketTracker checkTicket =  _context.TicketTrackers.Where(e => e.TicketId == ticketId).SingleOrDefault();
        return checkTicket;
    }
    public bool DeleteTicket(TicketTracker ticketTrackers)
    {
        _context.TicketTrackers.Remove(ticketTrackers);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateTicket(int ticketId)
    {
        var updateTicket = _context.Tickets.Where(x => x.TicketId == ticketId).SingleOrDefault();
        if (updateTicket != null)
        {
            updateTicket.TicketStatus = "Resolved";
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateSupportEng(int seId)
    {
        var supportEngineer = _context.SupportEngineers.Where(x => x.SeId == seId).SingleOrDefault();
        if (supportEngineer != null)
        {
            supportEngineer.IsAvailable = true;
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }
}

