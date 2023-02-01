using System;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Application.Services.Ticket ;

namespace WebAPI.Controllers;

public class TicketController : Controller
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {

        _ticketService = ticketService;
    }

    
    [HttpPost]
    [Route("createTicket/{id}"),Authorize(Roles = "User")]
    public string CreateTicket([FromBody] Tickets ticket, int? id)
    {
        return _ticketService.CreateTicket(ticket, id);
    }

    [HttpGet]
    [Route("getTickets/{id}"), Authorize(Roles = "usr")]
    public ActionResult GetTicket(int? id)
    {
        List<Tickets> tickets = _ticketService.GetTicket(id).Result;
        return Ok(tickets);

    }

    [HttpPut]
    [Route("updateTicket/{TicketNo}"), Authorize(Roles = "usr")]
    public string UpdateTicket([FromBody] Tickets ticket, int? ticketNo)
    {
        return _ticketService.UpdateTicket(ticket, ticketNo);
    }


    [HttpDelete]
    [Route("deleteTicket/{TicketNo}"), Authorize(Roles = "usr")]
    public string DeleteTicket(int? ticketNo)
    {
        return _ticketService.DeleteTicket(ticketNo);

    }


}

