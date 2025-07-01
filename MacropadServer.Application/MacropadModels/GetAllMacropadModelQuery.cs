using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadModels;
public sealed record GetAllMacropadModelQuery() : IRequest<Result<IQueryable<GetAllMacropadModelQueryResponse>>>;

public sealed class GetAllMacropadModelQueryResponse : EntityDto
{
    public string ModelName { get; set; } = string.Empty;
    public string ModelSerialNo { get; set; } = string.Empty;
    public string? ModelVersion { get; set; }
    public string? ModelDescription { get; set; }
    public string? DeviceSupport { get; set; }
    public int ButtonCount { get; set; } = 0;
    public int ModCount { get; set; } = 0;
    public bool IsScreenExist { get; set; }
    public string? ScreenType { get; set; }
    public double? ScreenSize { get; set; }
    public MacropadConnectionTypeEnum? ConnectionType { get; set; }
    public string? MicrocontrollerType { get; set; }
    public PowerTypeEnum? PowerType { get; set; }
    public bool? Rechargeable { get; set; }
    public string? CaseColor { get; set; }
    public string? CaseMaterial { get; set; }
    public string? CaseDescription { get; set; }
}

internal sealed class GetAllMacropadModelQueryHandler(
    IMacropadModelRepository macropadModelRepository) : IRequestHandler<GetAllMacropadModelQuery, Result<IQueryable<GetAllMacropadModelQueryResponse>>>
{
    public async Task<Result<IQueryable<GetAllMacropadModelQueryResponse>>> Handle(GetAllMacropadModelQuery request, CancellationToken cancellationToken)
    {
        var macropadModels = macropadModelRepository.GetAll()
            .Select(model => new GetAllMacropadModelQueryResponse
            {
                Id = model.Id,
                ModelName = model.ModelName,
                ModelSerialNo = model.ModelSerialNo,
                ModelVersion = model.ModelVersion,
                ModelDescription = model.ModelDescription,
                DeviceSupport = model.DeviceSupport,
                ButtonCount = model.ButtonCount,
                ModCount = model.ModCount,
                IsScreenExist = model.IsScreenExist,
                ScreenType = model.ScreenType,
                ScreenSize = model.ScreenSize,
                ConnectionType = model.ConnectionType,
                MicrocontrollerType = model.MicrocontrollerType,
                PowerType = model.PowerType,
                Rechargeable = model.Rechargeable,
                CaseColor = model.CaseColor,
                CaseMaterial = model.CaseMaterial,
                CaseDescription = model.CaseDescription
            })
            .AsQueryable();
        return Result<IQueryable<GetAllMacropadModelQueryResponse>>.Succeed(macropadModels);
    }
}
