using System;
using Domain.Entites;

namespace Application.Services.Register;

public interface IRegisterService
{
    public  Task<string> Register(User userInfo);
}

