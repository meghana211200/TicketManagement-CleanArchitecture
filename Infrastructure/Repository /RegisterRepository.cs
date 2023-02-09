using System;
using Application.Interface;
using Domain.DTO;
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
        _context.SupportEngineers.Add(supportEngineer);
        _context.SaveChanges();
        return true;
    }

    public bool AddUser(UserDTO userInfo)
    {
        User user = new User
        {
            UserEmail = userInfo.UserEmail,
            UserName = userInfo.UserName,
            UserPassword=userInfo.UserPassword,
            UserRole=userInfo.UserRole
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return true;
    }

    public User CheckEmail(string email)
    {
        var checkEmail = _context.Users.FirstOrDefault(x => x.UserEmail == email);
        return (checkEmail);
    }
}

