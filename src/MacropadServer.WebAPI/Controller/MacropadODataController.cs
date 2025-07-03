using ED.Result;
using MacropadServer.Application.MacropadDevices;
using MacropadServer.Application.MacropadModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace MacropadServer.WebAPI.Controller;
[Route("odata")]
[ApiController]
[EnableQuery]
//[Authorize(AuthenticationSchemes = "Bearer")]
public class MacropadODataController(
    ISender sender) : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();
        builder.EnableLowerCamelCase();
        builder.EntitySet<GetAllMacropadModelQueryResponse>("macropad-models");
        builder.EntitySet<GetMacropadModelByIdQueryResponse>("macropad-model");
        builder.EntitySet<GetAllMacropadDeviceQueryResponse>("macropad-devices");
        return builder.GetEdmModel();
    }

    [HttpGet("macropad-models")]
    public async Task<Result<IQueryable<GetAllMacropadModelQueryResponse>>> GetAllMacropadModel(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllMacropadModelQuery(), cancellationToken);
        return response;
    }

    [HttpGet("macropad-model")]
    public async Task<Result<GetMacropadModelByIdQueryResponse>> GetMacropadModel(Guid id, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetMacropadModelByIdQuery(id), cancellationToken);
        return response;
    }

    //[Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("macropad-devices")]
    public async Task<Result<IEnumerable<GetAllMacropadDeviceQueryResponse>>> GetAllMacropadDevice(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllMacropadDeviceQuery(), cancellationToken);
        return response;
    }

    //[Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("macropad-devices-user-id")]
    public async Task<Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>> GetAllMacropadDeviceByAppUserId(Guid appUserId, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllMacropadDeviceByAppUserIdQuery(appUserId), cancellationToken);
        return response;
    }

    //[Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("macropad-device")]
    public async Task<Result<GetMacropadDeviceQueryResponse>> GetMacropadDevice(string idOrST, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetMacropadDeviceQuery(idOrST), cancellationToken);
        return response;
    }
}
