using ED.Result;
using MacropadServer.Domain.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MacropadServer.Application.MacropadModels;
public sealed record GetAllMacropadModelQuery() : IRequest<Result<IQueryable<GetAllMacropadModelQueryResponse>>>;

public sealed class GetAllMacropadModelQueryResponse
{
    [Key]
    public string ModelSerialNo { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string? ModelVersion { get; set; }
    public string? ModelImage { get; set; }
}

internal sealed class GetAllMacropadModelQueryHandler(
    IMacropadModelRepository macropadModelRepository) : IRequestHandler<GetAllMacropadModelQuery, Result<IQueryable<GetAllMacropadModelQueryResponse>>>
{
    public Task<Result<IQueryable<GetAllMacropadModelQueryResponse>>> Handle(GetAllMacropadModelQuery request, CancellationToken cancellationToken)
    {
        var macropadModels = macropadModelRepository.GetAll()
            .Select(model => new GetAllMacropadModelQueryResponse
            {
                ModelName = model.ModelName,
                ModelSerialNo = model.ModelSerialNo,
                ModelVersion = model.ModelVersion,
                ModelImage = model.ModelImage
            })
            .AsQueryable();
        return Task.FromResult(Result<IQueryable<GetAllMacropadModelQueryResponse>>.Succeed(macropadModels));
    }
}
