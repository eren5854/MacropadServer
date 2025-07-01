using ED.Result;
using MacropadServer.Application.MacropadModels;
using MediatR;

namespace MacropadServer.WebAPI.Modules;

public static class MacropadModelModule
{
    public static void RegisterMacropadModelRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("macropad-models").WithTags("Macropad Models");

        group.MapPost("create",
            async (ISender sender, CreateMacropadModelCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("CreateMacropadModel");

        group.MapPost("update",
            async(ISender sender, UpdateMacropadModelCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("UpdateMacropadModel");

        group.MapPost("delete",
            async (ISender sender, DeleteMacropadModelCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("DeleteMacropadModel");
    }
}
