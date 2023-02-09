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

    public async Task<List<Ticket>> GetAllTicket()
    {
        List<Ticket> ticket = await _adminRepository.GetAllTickets();
        return (ticket);
    }

    public async Task<List<SupportEngineer>> GetAllAvailableSupportEng()
    {
        List<SupportEngineer> supportEngineers = await _adminRepository.GetAllAvailableSupportEng();
        return (supportEngineers);
    }

    public async Task<List<Ticket>> GetTicketsFilter(TicketFilterDTO ticketFilter)
    {
        List<Ticket> ticket = await _adminRepository.GetTicketsFilter(ticketFilter.ticketStatus);
        return (ticket);
    }

    public string AssignTicket(TicketTrackerDTO ticketTracker)
    {
        bool checkTicket =_adminRepository.CheckTicket(ticketTracker.ticketId);
        if (checkTicket == false)
        {
            var ticketTrackers = new TicketTracker
            {

                TicketTrackerSeId = ticketTracker.seId,
                TicketId = ticketTracker.ticketId,

            };
            bool addTicket = _adminRepository.AddTicket(ticketTrackers);

            bool updateTicket = _adminRepository.UpdateTicket( ticketTracker.ticketId);

            bool supportEngineer = _adminRepository.UpdateSupportEng(ticketTracker.seId);

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

