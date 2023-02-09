using System;
using Domain.Entites;
using bcrypt = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.AspNetCore.Mvc;
using Application.Interface;
using Domain.DTO;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon;
using System.Security.Authentication;

namespace Application.Services.Login;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IConfiguration _configuration;

    private readonly RegionEndpoint _region = RegionEndpoint.USEast1;

    public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
    {
        _loginRepository = loginRepository;
        _configuration = configuration;
    }

    public async Task<String> Login(LoginDTO login)
    {
     
        var cognito = new AmazonCognitoIdentityProviderClient(_region);

        var request = new AdminInitiateAuthRequest
        {
            UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
            ClientId = _configuration.GetValue<string>("Cognito:ClientId"),
            AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
        };

        request.AuthParameters.Add("USERNAME", login.email);
        request.AuthParameters.Add("PASSWORD", login.password);

        var response = await cognito.AdminInitiateAuthAsync(request);
        if(response!=null)
        {
        return (response.AuthenticationResult.IdToken);
        }

        throw new AuthenticationException("User doesn't exsits");

    }

}