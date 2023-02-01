using System;
using Application.Interface;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RegisterRepository : IRegisterRepository
{
    private readonly TicketManagementDbContext _context;

    public RegisterRepository(TicketManagementDbContext context)
    {
        _context = context;
    }

    public bool AddSupportEngineer(SupportEngineer supportEngineer)
    {
        _context.SupportEngineer.Add(supportEngineer);
        _context.SaveChanges();
        return true;
    }

    public bool AddUser(User userInfo)
    {
        _context.User.Add(userInfo);
        _context.SaveChanges();
        return true;
    }

    public User CheckEmail(string email)
    {
        var checkEmail = _context.User.FirstOrDefault(x => x.user_email == email);
        return (checkEmail);
    }
}

