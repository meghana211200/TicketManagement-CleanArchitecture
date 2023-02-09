using System;
using Application.Interface;
using Application.Services.Tickets;
using Domain.Entites;
using Moq;
using Org.BouncyCastle.Asn1.X509;

namespace TicketManagementTest;

public class TicketServiceTestMOQ
{
	[Fact]
    public void CreateTicketServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1001;

        mock.Setup(t => t.CheckUser(It.IsAny<int>())).Returns(new User() { UserId= id});
        Ticket ticket = new Ticket
        {
            Complaint = "Issue 1"
        };
        mock.Setup(t => t.AddIssue(It.IsAny<Ticket>())).Returns(true);

        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.CreateTickets(ticket, id);
        Assert.Equal("Hey 1001, Your complaint Is Raised Successfully", ticketReturned);
    }
    [Fact]
    public void CreateTicketFailServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1100;

        mock.Setup(t => t.CheckUser(It.IsAny<int>())).Returns(new User() { UserId = id });
        Ticket ticket = new Ticket
        {
            Complaint = "Issue 1"
        };
        mock.Setup(t => t.AddIssue(It.IsAny<Ticket>())).Returns(false);

        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.CreateTickets(ticket, id);
        Assert.Equal("Ticket not raised", ticketReturned);
    }

    [Fact]
    public void CreateTicketUserServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1100;

        mock.Setup(t => t.CheckUser(It.IsAny<int>()));
        Ticket ticket = new Ticket
        {
            Complaint = "Issue 1"
        };
        mock.Setup(t => t.AddIssue(It.IsAny<Ticket>())).Returns(false);

        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.CreateTickets(ticket, id);
        Assert.Equal("User not present", ticketReturned);
    }

    [Fact]
    public void UpdateTicketsServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1;
        var ticketData = new Ticket
        {
            Complaint = "Issue 1",
        };
        mock.Setup(t => t.UpdateTicket(It.IsAny<Ticket>(),It.IsAny<int>())).Returns(true);

        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.UpdateTickets(ticketData, id);
        Assert.Equal("Issue Ticket Is Being Updated", ticketReturned);
    }

    [Fact]
    public void UpdateTicketsFailServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1;
        var ticketData = new Ticket
        {
            Complaint = "Issue 1",
        };
        mock.Setup(t => t.UpdateTicket(It.IsAny<Ticket>(), It.IsAny<int>())).Returns(false);

        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.UpdateTickets(ticketData, id);
        Assert.Equal("Issue Ticket was not updates", ticketReturned);
    }

    [Fact]
    public void DeleteTicketTicketNotPresentServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1;
        mock.Setup(t => t.CheckTicket(It.IsAny<int>())).Returns(false);
        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.DeleteTicket(id);
        Assert.Equal("Ticket not present", ticketReturned);
    }

    [Fact]
    public void DeleteTicketServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1;
        mock.Setup(t => t.CheckTicket(It.IsAny<int>())).Returns(true);
        mock.Setup(t => t.DeleteTicket(It.IsAny<int>())).Returns(true);
        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.DeleteTicket(id);
        Assert.Equal("Your Ticket is Deleted Successfully", ticketReturned);
    }

    [Fact]
    public void DeleteTicketFailServiceTest()
    {
        var mock = new Mock<ITicketRepository>();
        int id = 1;
        mock.Setup(t => t.CheckTicket(It.IsAny<int>())).Returns(true);
        mock.Setup(t => t.DeleteTicket(It.IsAny<int>())).Returns(false);
        TicketsService ticketService = new TicketsService(mock.Object);
        var ticketReturned = ticketService.DeleteTicket(id);
        Assert.Equal("Your Ticket is not Deleted Successfully", ticketReturned);
    }
}

