using System;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Application.Services.Tickets ;
using Application.Interface;

namespace WebAPI.Controllers;

public class TicketController : Controller
{
    private readonly ITicketsService _ticketService;

    public TicketController(ITicketsService ticketService)
    {

        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("createTicket/{id}"),Authorize(Roles = "User")]
    public string CreateTicket([FromBody] Ticket ticket, int id)
    {
        return _ticketService.CreateTickets(ticket, id);
    }

    [HttpGet]
    [Route("getTickets/{id}"), Authorize(Roles = "User")]
    public ActionResult GetTicket(int? id)
    {
        List<Ticket> tickets = _ticketService.GetTicket(id).Result;
        return Ok(tickets);
    }

    [HttpPut]
    [Route("updateTicket/{TicketNo}"), Authorize(Roles = "User")]
    public string UpdateTicket([FromBody] Ticket ticket, int? ticketNo)
    {
        return _ticketService.UpdateTickets(ticket, ticketNo);
    }


    [HttpDelete]
    [Route("deleteTicket/{TicketNo}"), Authorize(Roles = "usr")]
    public string DeleteTicket(int ticketNo)
    {
        return _ticketService.DeleteTicket(ticketNo);
    }
}

