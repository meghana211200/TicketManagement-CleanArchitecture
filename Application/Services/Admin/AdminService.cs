using System;
using Application.Interface;
using Domain.DTO;
using Domain.Entites;

namespace Application.Services.Admin;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {

        _adminRepository = adminRepository;
    }

    public async Task<List<Tickets>> GetAllTicket()
    {
        List<Tickets> ticket = await _adminRepository.GetAllTickets();
        return (ticket);
    }

    public async Task<List<SupportEngineer>> GetAllAvailableSupportEng()
    {
        List<SupportEngineer> supportEngineers = await _adminRepository.GetAllAvailableSupportEng();
        return (supportEngineers);
    }

    public async Task<List<Tickets>> GetTicketsFilter(TicketFilterDTO ticketFilter)
    {
        List<Tickets> ticket = await _adminRepository.GetTicketsFilter(ticketFilter.ticketStatus);
        return (ticket);
    }

    public string AssignTicket(TicketTrackerDTO ticketTracker)
    {
        bool checkTicket =_adminRepository.CheckTicket(ticketTracker.ticket_id);
        if (checkTicket == false)
        {
            var ticketTrackers = new TicketTracker
            {

                ticketTracker_se_id = ticketTracker.se_id,
                ticket_id = ticketTracker.ticket_id,

            };
            bool addTicket = _adminRepository.AddTicket(ticketTrackers);

            bool updateTicket = _adminRepository.UpdateTicket( ticketTracker.ticket_id);

            bool supportEngineer = _adminRepository.UpdateSupportEng(ticketTracker.se_id);

            if(addTicket && updateTicket && supportEngineer)
            {
                return ("Hey " + ticketTrackers + ", Your complaint Is Raised Successfully");
            }
            else
            {
                return ("Couldn't assign ticket");
            }
        }
        else
        {
            return ("Incorrect UserID !");
        }
    }
}

