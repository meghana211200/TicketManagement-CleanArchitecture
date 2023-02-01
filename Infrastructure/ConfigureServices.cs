using System;
using System.Configuration;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Interface;
using Infrastructure.Repository;

namespace Microsoft.Extensions.DependencyInjection;


public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketManagementDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("LocalConnection")));
        services.AddScoped<IRegisterRepository, RegisterRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>(); 
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ISupportEngineerRepository, SupportEngineerRepository>();

        return services;
    }
}



    