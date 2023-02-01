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

    public async Task<List<Tickets>> GetAllTickets()
    {
        List<Tickets> ticket = await _context.Tickets.ToListAsync();
        return (ticket);
    }

    public async Task<List<SupportEngineer>> GetAllAvailableSupportEng()
    {
        List<SupportEngineer> supportEngineers = await _context.SupportEngineer.Where(x => x.isAvailable == true).ToListAsync();
        return (supportEngineers);
    }

    public async Task<List<Tickets>> GetTicketsFilter(string ticketFilter)
    {
        List<Tickets> ticket = await _context.Tickets.Where(x => x.ticket_status == ticketFilter).ToListAsync();
        return (ticket);
    }

    public bool CheckTicket(int ticket_id)
    {
       var checkTicket= _context.TicketTracker.FirstOrDefault(x => x.ticket_id == ticket_id);
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
        _context.TicketTracker.Add(ticketTrackers);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateTicket(int ticket_id)
    {
        var updateTicket = _context.Tickets.Where(x => x.ticket_id == ticket_id).SingleOrDefault();
        if (updateTicket != null)
        {
            updateTicket.ticket_status = "Assigned";
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
            supportEngineer.isAvailable = false;
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

}

