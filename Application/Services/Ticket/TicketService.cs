using System;
using Application.Interface;
using Domain.Entites;

namespace Application.Services.Ticket;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {

        _ticketRepository = ticketRepository;
    }



    public string CreateTicket(Tickets ticket, int? id)
    {
        var user = _ticketRepository.CheckUser(id);
        var complaint = new Tickets
        {
            ticket_user_id = user.user_id,
            complaint = ticket.complaint,
            ticket_status = "New"
        };
        bool addedUser = _ticketRepository.AddIssue(complaint);
        return ("Hey " + complaint.ticket_user_id + ", Your complaint Is Raised Successfully");
    }

    public  Task<List<Tickets>> GetTicket(int? id)
    {
        Task<List<Tickets>> ticket = _ticketRepository.GetTickets(id);
        return (ticket);
    }

    public string UpdateTicket(Tickets ticket, int? ticketNo)
    {
        bool updateTicket = _ticketRepository.UpdateTicket(ticket, ticketNo);
        return ("Issue Ticket Is Being Updated");

    }

    public string DeleteTicket(int? ticketNo)
    {
        bool ticket = _ticketRepository.CheckTicket(ticketNo);
        if (ticket == false)
        {
            return ("Ticket not present");
        }
        else
        {
            bool tickets = _ticketRepository.DeleteTicket(ticketNo);
            return ("Your Ticket  is Deleted Successfully");
        }
    }
}


