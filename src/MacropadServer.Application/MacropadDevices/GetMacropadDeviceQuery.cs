using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.DTOs;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Application.MacropadDevices;
public sealed record GetMacropadDeviceQuery(string IdOrST) : IRequest<Result<GetMacropadDeviceQueryResponse>>;

public sealed class GetMacropadDeviceQueryResponse : EntityDto
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }

    public GetMacropadModelDto? MacropadModel { get; set; }
    public IEnumerable<GetMacropadInputDto>? MacropadInputs { get; set; }
    public IEnumerable<MacropadEyeAnimation>? MacropadEyeAnimations { get; set; }
}

internal sealed class GetMacropadDeviceQueryHandler(
    IMacropadDeviceRepository macropadRepository) : IRequestHandler<GetMacropadDeviceQuery, Result<GetMacropadDeviceQueryResponse>>
{
    public async Task<Result<GetMacropadDeviceQueryResponse>> Handle(GetMacropadDeviceQuery request, CancellationToken cancellationToken)
    {
        IQueryable<MacropadDevice> query = macropadRepository
                                            .GetAll()
                                            .Include(i => i.MacropadModel)
                                            .Include(m => m.MacropadInputs)
                                            .Include(m => m.MacropadEyeAnimations);
        query = Guid.TryParse(request.IdOrST, out var id) ? query.Where(w => w.Id == id) : query.Where(w => w.MacropadSecretToken == request.IdOrST);
        var macropad = await query.Select(macropad => new GetMacropadDeviceQueryResponse
        {
            Id = macropad.Id,
            MacropadName = macropad.MacropadName,
            MacropadSecretToken = macropad.MacropadSecretToken,
            IsEyeAnimationEnabled = macropad.IsEyeAnimationEnabled,
            CreatedAt = macropad.CreatedAt,
            CreatedBy = macropad.CreatedBy,
            UpdatedAt = macropad.UpdatedAt,
            UpdatedBy = macropad.UpdatedBy,
            DeleteAt = macropad.DeleteAt,
            DeleteBy = macropad.DeleteBy,
            MacropadModel = macropad.MacropadModel != null
                ? new GetMacropadModelDto(
                    macropad.MacropadModel.ModelName,
                    macropad.MacropadModel.ModelSerialNo,
                    macropad.MacropadModel.ModelVersion,
                    macropad.MacropadModel.ModelDescription,
                    macropad.MacropadModel.ModelImage,
                    macropad.MacropadModel.DeviceSupport,
                    macropad.MacropadModel.ButtonCount,
                    macropad.MacropadModel.ModCount,
                    macropad.MacropadModel.IsScreenExist,
                    macropad.MacropadModel.ScreenType,
                    macropad.MacropadModel.ScreenSize,
                    macropad.MacropadModel.ConnectionType,
                    macropad.MacropadModel.MicrocontrollerType,
                    macropad.MacropadModel.PowerType,
                    macropad.MacropadModel.Rechargeable,
                    macropad.MacropadModel.CaseColor,
                    macropad.MacropadModel.CaseMaterial,
                    macropad.MacropadModel.CaseDescription
                )
                : null,
            MacropadInputs = macropad.MacropadInputs!
                 .OrderBy(i => i.InputIndex)
                 .Select(i => new GetMacropadInputDto(
                     i.Id,
                     i.InputName,
                     i.InputIndex,
                     i.ModIndex,
                     i.InputBitMap,
                     i.InputType,
                     i.Item1,
                     i.Item2,
                     i.Item3,
                     i.Item4))
        }).FirstOrDefaultAsync(cancellationToken);
        if (macropad is null) return Result<GetMacropadDeviceQueryResponse>.Failure("Macropad cihazı bulunamadı.");
        return Result<GetMacropadDeviceQueryResponse>.Succeed(macropad);
    }
}