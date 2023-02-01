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

    public async Task<string> Register([FromBody] User userInfo)
    {
        var cognito = new AmazonCognitoIdentityProviderClient(_region);

        var request = new SignUpRequest
        {
            ClientId = _configuration.GetValue<string>("Cognito:ClientId"),
            Password = userInfo.user_password,
            Username = userInfo.user_email
        };

        var emailAttribute = new AttributeType
        {
            Name = "email",
            Value = userInfo.user_email
        };
        request.UserAttributes.Add(emailAttribute);

        var response = await cognito.SignUpAsync(request);

        if (userInfo.user_role == "usr")
        {
            var groupRequest = new AdminAddUserToGroupRequest
            {
                GroupName = "User",
                UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
                Username = userInfo.user_email
            };
            var groupAddedResponse = await cognito.AdminAddUserToGroupAsync(groupRequest);

        }
        else if (userInfo.user_role == "se")
        {
            var groupRequest = new AdminAddUserToGroupRequest
            {
                GroupName = "SupportEngineer",
                UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
                Username = userInfo.user_email
            };
            var groupAddedResponse = await cognito.AdminAddUserToGroupAsync(groupRequest);
        }

        var AdminConfirmSignup = new AdminConfirmSignUpRequest
        {
            UserPoolId = _configuration.GetValue<string>("Cognito:UserPoolId"),
            Username = userInfo.user_email
        };
        await cognito.AdminConfirmSignUpAsync(AdminConfirmSignup);

        var checkEmail = _registerRepository.CheckEmail(userInfo.user_email);
        if (checkEmail == null)
        {
            if (userInfo.user_email.Contains("@gmail.com") && userInfo.user_password.Length > 8)
            {
                userInfo.user_password = bcrypt.HashPassword(userInfo.user_password, 12);
                //_context.User.Add(userInfo);
                //_context.SaveChanges();
                bool addedUser = _registerRepository.AddUser(userInfo);
                if (userInfo.user_role == "se")
                {
                    var supportEngineer = new SupportEngineer
                    {
                        se_user_id = userInfo.user_id,
                        isAvailable = true

                    };
                    //_context.SupportEngineer.Add(supportEngineer);
                    //_context.SaveChanges();
                    bool addedSupportEngineer = _registerRepository.AddSupportEngineer(supportEngineer);
                }
                return ("User added successfully");
            }
            else
            {
                return ("Incorrect Email");
            }

        }
        else
        {
            return ("User already present");
        }
    }
}