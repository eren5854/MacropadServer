using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Infrastructure.Context;
using MacropadServer.Infrastructure.Options;
using MacropadServer.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MacropadServer.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //DotNetEnv.Env.Load();
        //var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
        //services.AddDbContext<ApplicationDbContext>(options =>
        //{
        //    options.UseNpgsql(connectionString);
        //});

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DEFAULT_CONNECTION"));
        });

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.SignIn.RequireConfirmedEmail = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        //services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.ConfigureOptions<JwtTokenOptionsSetupConfiguration>();
        services.AddAuthentication()
            .AddJwtBearer();
        services.AddAuthorizationBuilder();

        services.AddScoped<JwtProvider>();

        //services.AddChatClient(new OllamaChatClient(new Uri("http://192.168.68.154:14001"), "llama3.2:1b"));

        services.Scan(action =>
        {
            action
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .AsImplementedInterfaces()
            .WithScopedLifetime();
        });
        return services;
    }
}
