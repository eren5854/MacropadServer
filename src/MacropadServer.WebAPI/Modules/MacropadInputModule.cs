using ED.Result;
using MacropadServer.Application.MacropadInputs;
using MediatR;

namespace MacropadServer.WebAPI.Modules;

public static class MacropadInputModule
{
    public static void RegisterMacropadInputRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("macropad-inputs")
            .WithTags("Macropad Inputs")
            .RequireAuthorization();
        group.MapPost("create",
            async (ISender sender, CreateMacropadInputCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("CreateMacropadInput");
        group.MapPost("update",
            async (ISender sender, UpdateMacropadInputCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("UpdateMacropadInput");
        group.MapPost("delete",
            async (ISender sender, DeleteMacropadInputCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("DeleteMacropadInput");
    }
}
