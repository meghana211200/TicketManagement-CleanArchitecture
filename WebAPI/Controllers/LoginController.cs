using System;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Application.Services.Login;
using Domain.DTO;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
 
    private readonly ILoginService _loginService;
    private readonly IConfiguration _configuration;

    public LoginController(ILoginService loginService, IConfiguration configuration)
    {

        _loginService = loginService;
        _configuration = configuration;

    }

    [HttpGet]
    [Route("hello")]
    public string Hello()
    {
        return ("Hello");
    }


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO login)
    {
        var response = _loginService.Login(login).Result;
        //return Ok(message);
        //var cognito = new AmazonCognitoIdentityProviderClient(_region);

        //var request = new AdminInitiateAuthRequest
        //{
        //    UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
        //    ClientId = _configuration.GetValue<string>("Cognito:ClientId"),
        //    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
        //};

        //request.AuthParameters.Add("USERNAME", login.email);
        //request.AuthParameters.Add("PASSWORD", login.password);

        //var response = await cognito.AdminInitiateAuthAsync(request);

        return Ok(response);
    }

}
