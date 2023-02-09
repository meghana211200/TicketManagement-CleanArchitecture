using System;
using Domain.DTO;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Application.Services.Admin;

namespace WebAPI.Controllers;

public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {

        _adminService = adminService;
    }

    [HttpGet]
    [Route("getAllTickets/{id}"), Authorize(Roles = "adm")]
    public ActionResult<List<Ticket>> GetAllTicket()
    {
        List<Ticket> tickets = _adminService.GetAllTicket().Result;
        return Ok(tickets);
    }

    [HttpGet]
    [Route("getAllAvailableSupportEng/{id}"), Authorize(Roles = "adm")]
    public ActionResult<List<SupportEngineer>> GetAllAvailableSupportEng()
    {
        List<SupportEngineer> supportEngineers = _adminService.GetAllAvailableSupportEng().Result;
        return Ok(supportEngineers);
    }

    [HttpGet]
    [Route("getTicketsFilter/{id}"), Authorize(Roles = "adm")]
    public ActionResult<List<Ticket>> GetTicketsFilter([FromBody] TicketFilterDTO ticketFilter)
    {
        List<Ticket> tickets = _adminService.GetTicketsFilter(ticketFilter).Result;
        return Ok(tickets);
    }

    [HttpPost]
    [Route("assignTicket/{id}"), Authorize(Roles = "adm")]
    public ActionResult<Task<string>> AssignTicket([FromBody] TicketTrackerDTO ticketTracker, int? id)
    {
        var message = _adminService.AssignTicket(ticketTracker);
        return Ok(message);
    }
}