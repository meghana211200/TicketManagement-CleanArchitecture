using System;
using Domain.DTO;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Register;

public interface IRegisterService
{
    public Task<string> Register(UserDTO userInfo);
}

