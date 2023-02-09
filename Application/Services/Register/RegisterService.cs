using System;
using Domain.Entites;
using bcrypt = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.AspNetCore.Mvc;
using Application.Interface;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Amazon;
using System.Security.Authentication;
using Domain.DTO;

namespace Application.Services.Register;

public class RegisterService : IRegisterService
{
    private readonly IRegisterRepository _registerRepository;
    private readonly IConfiguration _configuration;

    public RegisterService(IRegisterRepository registerRepository, IConfiguration configuration)
    {

        _registerRepository = registerRepository;
        _configuration = configuration;
    }

    private readonly RegionEndpoint _region = RegionEndpoint.USEast1;

    public async Task<string> Register([FromBody] UserDTO userInfo)
    {
        var cognito = new AmazonCognitoIdentityProviderClient(_region);

        var request = new SignUpRequest
        {
            ClientId = _configuration.GetValue<string>("Cognito:ClientId"),
            Password = userInfo.UserPassword,
            Username = userInfo.UserEmail
        };

        var emailAttribute = new AttributeType
        {
            Name = "email",
            Value = userInfo.UserEmail
        };
        request.UserAttributes.Add(emailAttribute);

        var response = await cognito.SignUpAsync(request);

        if (userInfo.UserRole == "usr")
        {
            var groupRequest = new AdminAddUserToGroupRequest
            {
                GroupName = "User",
                UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
                Username = userInfo.UserEmail
            };
            var groupAddedResponse = await cognito.AdminAddUserToGroupAsync(groupRequest);

        }
        else if (userInfo.UserRole == "se")
        {
            var groupRequest = new AdminAddUserToGroupRequest
            {
                GroupName = "SupportEngineer",
                UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
                Username = userInfo.UserEmail
            };
            var groupAddedResponse = await cognito.AdminAddUserToGroupAsync(groupRequest);
        }

        var AdminConfirmSignup = new AdminConfirmSignUpRequest
        {
            UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
            Username = userInfo.UserEmail
        };
        await cognito.AdminConfirmSignUpAsync(AdminConfirmSignup);

        var checkEmail = _registerRepository.CheckEmail(userInfo.UserEmail);
        if (checkEmail == null)
        {
            if (userInfo.UserEmail.Contains("@gmail.com") && userInfo.UserPassword.Length > 8)
            {
                userInfo.UserPassword = bcrypt.HashPassword(userInfo.UserPassword, 12);
                //_context.User.Add(userInfo);
                //_context.SaveChanges();
                bool addedUser = _registerRepository.AddUser(userInfo);
                if (userInfo.UserRole == "se")
                {
                    var supportEngineer = new SupportEngineer
                    {
                        SeUserId= userInfo.UserId,
                        IsAvailable = true

                    };
                    //_context.SupportEngineer.Add(supportEngineer);
                    //_context.SaveChanges();
                    bool addedSupportEngineer = _registerRepository.AddSupportEngineer(supportEngineer);
                }
                return ("User added successfully");
            }
            else
            {
                throw new AuthenticationException("Incorrect Email");
            }

        }
        else
        {
            throw new AuthenticationException("User already present");
        }
    }
}