using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Swashbuckle.AspNetCore.Filters;
using System.Drawing;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var clientId = builder.Configuration.GetValue<string>("Cognito:ClientId");
var authorityUrl = builder.Configuration.GetValue<string>("Cognito:AuthorityUrl");

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer(options =>
 {
     options.Audience = clientId;
     options.Authority = authorityUrl;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         RoleClaimType = "cognito:groups",
     };
 });



builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("User", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(c => c.Type == "cognito:groups" && c.Value.Contains("User"))));
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
