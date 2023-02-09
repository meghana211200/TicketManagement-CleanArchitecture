using System;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Services.Register;
using Domain.DTO;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAPI.Controllers;

[ApiController]
public class RegisterController : Controller
{
    private readonly IRegisterService _registerService;
    private readonly IConfiguration _configuration;

    public RegisterController(IRegisterService registerService, IConfiguration configuration)
    {
        _registerService = registerService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("register")]
    public  Task<string> Register([FromBody] UserDTO userInfo)
    { 
        Task<string> resp=_registerService.Register(userInfo);
        return (resp); 
    }

}

