using System;
using Domain.Entites;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class TicketManagementDbContext : DbContext
{
    public TicketManagementDbContext(DbContextOptions<TicketManagementDbContext> options) : base(options)
    {
    }
    public DbSet<User> User { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<SupportEngineer> SupportEngineer { get; set; }
    public DbSet<TicketTracker> TicketTracker { get; set; }
}