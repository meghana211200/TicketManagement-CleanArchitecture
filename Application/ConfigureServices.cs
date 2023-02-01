using System;
using System.Reflection;
using Application.Services.Admin;
using Application.Services.Login;
using Application.Services.Register;
using Application.Services.SupportEngineers;
using Application.Services.Ticket;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ISupportEngineerService, SupportEngineerService>();
        return services;
    }
}