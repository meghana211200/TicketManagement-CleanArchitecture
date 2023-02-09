using System;
using Application.Interface;
using Domain.DTO;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly TicketManagementDbContext _context;

    public AdminRepository(TicketManagementDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTickets()
    {
        List<Ticket> ticket = await _context.Tickets.ToListAsync();
        return (ticket);
    }

    public async Task<List<SupportEngineer>> GetAllAvailableSupportEng()
    {
        List<SupportEngineer> supportEngineers = await _context.SupportEngineers.Where(x => x.IsAvailable == true).ToListAsync();
        return (supportEngineers);
    }

    public async Task<List<Ticket>> GetTicketsFilter(string ticketFilter)
    {
        List<Ticket> ticket = await _context.Tickets.Where(x => x.TicketStatus == ticketFilter).ToListAsync();
        return (ticket);
    }

    public bool CheckTicket(int ticketId)
    {
       var checkTicket= _context.TicketTrackers.FirstOrDefault(x => x.TicketId == ticketId);
        if(checkTicket != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AddTicket(TicketTracker ticketTrackers)
    {
        _context.TicketTrackers.Add(ticketTrackers);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateTicket(int ticketId)
    {
        var updateTicket = _context.Tickets.Where(x => x.TicketId == ticketId).SingleOrDefault();
        if (updateTicket != null)
        {
            updateTicket.TicketStatus = "Assigned";
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
            supportEngineer.IsAvailable = false;
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

}

