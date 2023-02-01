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
        //var checkEmail = _loginRepository.CheckEmail(login.email);
        //if (checkEmail != null)
        //{
        //    if (bcrypt.Verify(login.password, checkEmail.user_password))
        //    {
        //        var token = CreateToken(checkEmail);
        //        return (token);
        //    }
        //    else
        //    {
        //        return ("Wrong Password");
        //    }
        //}
        //else
        //{
        //    return ("Invalid User");
        //}
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

        return (response.AuthenticationResult.IdToken);
    }

    //private string CreateToken(User user)
    //{
    //    List<Claim> claims = new List<Claim>
    //    {
    //        new Claim("ID",user.user_id.ToString()),
    //        new Claim(ClaimTypes.Email, user.user_email),
    //        new Claim(ClaimTypes.Role, user.user_role)
    //    };

    //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
    //        _configuration.GetSection("SecretKey:Token").Value));

    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    //    var token = new JwtSecurityToken(
    //        claims: claims,
    //        expires: DateTime.Now.AddDays(7),
    //        signingCredentials: creds);

    //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    //    return jwt;
    //}

}