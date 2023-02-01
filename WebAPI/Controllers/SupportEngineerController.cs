using System;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Application.Services.SupportEngineers;

namespace WebAPI.Controllers;

public class SupportEngineerController : Controller
{
    private readonly ISupportEngineerService _supportEngineerService;

    public SupportEngineerController(ISupportEngineerService supportEngineerService)
    {

        _supportEngineerService = supportEngineerService;
    }


    [HttpPut]
    [Route("closeTicket/{id}"), Authorize(Roles = "se")]
    public ActionResult<Task<string>> CloseTicket([FromBody] TicketTrackerDTO ticketTracker)
    {

        var message = _supportEngineerService.CloseTicket(ticketTracker);
        return Ok(message);

    }



}