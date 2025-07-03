using ED.Result;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MacropadServer.Application.MacropadModels;
public sealed record GetMacropadModelByIdQuery(Guid Id) : IRequest<Result<GetMacropadModelByIdQueryResponse>>;

public sealed class GetMacropadModelByIdQueryResponse
{
    [Key]
    public string ModelSerialNo { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string? ModelVersion { get; set; }
    public string? ModelDescription { get; set; }
    public string? ModelImage { get; set; }
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

internal sealed class GetMacropadModelByIdQueryHandler(
    IMacropadModelRepository macropadModelRepository) : IRequestHandler<GetMacropadModelByIdQuery, Result<GetMacropadModelByIdQueryResponse>>
{
    public async Task<Result<GetMacropadModelByIdQueryResponse>> Handle(GetMacropadModelByIdQuery request, CancellationToken cancellationToken)
    {
        var macropadModel = await macropadModelRepository
            .Where(w => w.Id == request.Id)
            .Select(model => new GetMacropadModelByIdQueryResponse
            {
                ModelName = model.ModelName,
                ModelSerialNo = model.ModelSerialNo,
                ModelVersion = model.ModelVersion,
                ModelDescription = model.ModelDescription,
                ModelImage = model.ModelImage,
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
            .FirstOrDefaultAsync(cancellationToken);
        if (macropadModel is null) return Result<GetMacropadModelByIdQueryResponse>.Failure("Makropad model bulunamadı");
        return Result<GetMacropadModelByIdQueryResponse>.Succeed(macropadModel);
    }
}
