using System;
using Application.Interface;
using Application.Services.Tickets;
using Domain.Entites;

namespace Application.Services.Tickets;

public class TicketsService : ITicketsService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketsService(ITicketRepository ticketRepository)
    {

        _ticketRepository = ticketRepository;
    }



    public string CreateTickets(Ticket ticket, int id)
    {
        try
        {

        var user = _ticketRepository.CheckUser(id);
        if(user!=null)
        {
        var complaint = new Ticket
        {
            TicketUserId = user.UserId,
            Complaint = ticket.Complaint,
            TicketStatus = "New"
        };
        bool addedUser = _ticketRepository.AddIssue(complaint);
                if(addedUser==true)
                {
        return ("Hey " + complaint.TicketUserId + ", Your complaint Is Raised Successfully");
                }
                else
                {
                    return ("Ticket not raised");
                }
        }
        else
        {
            return ("User not present");
        }
        }
        catch(Exception)
        {
            return ("Exception thrown");
        }
    }

    public Task<List<Ticket>> GetTicket(int? id)
    {
        Task<List<Ticket>> ticket = _ticketRepository.GetTickets(id);
        return (ticket);
    }

    public string UpdateTickets(Ticket ticket, int? ticketNo)
    {
        bool updateTicket = _ticketRepository.UpdateTicket(ticket, ticketNo);
        if(updateTicket==true)
        {
        return ("Issue Ticket Is Being Updated");
        }
        else
        {
            return ("Issue Ticket was not updates");
        }

    }

    public string DeleteTicket(int ticketNo)
    {
        bool ticket = _ticketRepository.CheckTicket(ticketNo);
        if (ticket == false)
        {
            return ("Ticket not present");
        }
        else
        {
            bool tickets = _ticketRepository.DeleteTicket(ticketNo);
            if(tickets == true)
            {
            return ("Your Ticket is Deleted Successfully");
            }
            else
            {
                return ("Your Ticket is not Deleted Successfully");
            }
        }
    }

 
}



