using FluentValidation;
using MacropadServer.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace MacropadServer.Application;
public static class ApplicationRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly);
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddAutoMapper(typeof(ApplicationRegistrar).Assembly);
        services.AddValidatorsFromAssembly(typeof(ApplicationRegistrar).Assembly);
        return services;
    }
}
