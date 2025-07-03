using ED.Result;
using MacropadServer.Application.MacropadDevices;
using MediatR;

namespace MacropadServer.WebAPI.Modules;

public static class MacropadDeviceModule
{
    public static void RegisterMacropadDeviceRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("macropad-devices")
            .WithTags("Macropad Devices")
            //.RequireAuthorization()
            ;
        group.MapPost("create",
            async (ISender sender, CreateMacropadDeviceCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("CreateMacropadDevice");
        group.MapPost("update",
            async (ISender sender, UpdateMacropadDeviceCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("UpdateMacropadDevice");
        group.MapPost("delete",
            async (ISender sender, DeleteMacropadDeviceCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("DeleteMacropadDevice");
    }
}
