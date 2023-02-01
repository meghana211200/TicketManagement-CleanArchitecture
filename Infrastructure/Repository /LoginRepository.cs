using System;
using Application.Interface;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class LoginRepository : ILoginRepository
{
    private readonly TicketManagementDbContext _context;

    public LoginRepository(TicketManagementDbContext context)
    {
        _context = context;
    }

    public User CheckEmail(string email)
    {
        var checkEmail = _context.User.FirstOrDefault(x => x.user_email == email);
        return (checkEmail);
    }
}

