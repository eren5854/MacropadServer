using ED.Result;
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
public class MacropadODataController(
    ISender sender) : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();
        builder.EnableLowerCamelCase();
        builder.EntitySet<GetAllMacropadModelQueryResponse>("macropad-models");
        return builder.GetEdmModel();
    }

    [HttpGet("macropad-models")]
    public async Task<Result<IQueryable<GetAllMacropadModelQueryResponse>>> GetAllMacropadModel(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllMacropadModelQuery(), cancellationToken);
        return response;
    }
}
