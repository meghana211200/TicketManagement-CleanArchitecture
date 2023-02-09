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
        bool updateTicket = _supportEngineerRepository.UpdateTicket(ticketTracker.ticketId);

        bool supportEngineer = _supportEngineerRepository.UpdateSupportEng(ticketTracker.seId);


        TicketTracker ticket = _supportEngineerRepository.CheckTicket(ticketTracker.ticketId);
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
