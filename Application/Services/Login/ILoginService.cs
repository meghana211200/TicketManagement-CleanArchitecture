using System;
using Domain.DTO;

namespace Application.Services.Login;

public interface ILoginService
{

    public Task<String> Login(LoginDTO login);

}

