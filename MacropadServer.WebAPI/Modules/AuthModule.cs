using ED.Result;
using MacropadServer.Application.Auth;
using MediatR;

namespace MacropadServer.WebAPI.Modules;

public static class AuthModule
{
    public static void RegisterAuthRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/auth").WithTags("Auth");
        group.MapPost("login",
            async (ISender sender, LoginCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<LoginCommandResponse>>()
            .WithName("Login");

        group.MapPost("register",
            async (ISender sender, RegisterCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.BadRequest(response);
            })
            .Produces<Result<string>>()
            .WithName("Register");
    }
}
