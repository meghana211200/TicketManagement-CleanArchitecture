using System;
using Application.Interface;
using Domain.DTO;
using Domain.Entites;

namespace Application.Services.SupportEngineers;

public class SupportEngineerService : ISupportEngineerService
{
    private readonly ISupportEngineerRepository _supportEngineerRepository;

    public SupportEngineerService(ISupportEngineerRepository supportEngineerRepository)
    {
        _supportEngineerRepository = supportEngineerRepository;
    }

    public string CloseTicket(TicketTrackerDTO ticketTracker)
    {
        bool updateTicket = _supportEngineerRepository.UpdateTicket(ticketTracker.ticket_id);

        bool supportEngineer = _supportEngineerRepository.UpdateSupportEng(ticketTracker.se_id);


        TicketTracker ticket = _supportEngineerRepository.CheckTicket(ticketTracker.ticket_id);
        if (ticket == null)
        {
            return ("Ticket not present");
        }
        else
        {

            bool ticketDeleted = _supportEngineerRepository.DeleteTicket(ticket);

            return ("Your Ticket  is Deleted Successfully");

        }
    }
}
