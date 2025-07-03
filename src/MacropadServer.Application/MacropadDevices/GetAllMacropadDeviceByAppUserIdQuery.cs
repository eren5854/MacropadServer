using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.DTOs;
using MacropadServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Application.MacropadDevices;
public sealed record GetAllMacropadDeviceByAppUserIdQuery(Guid AppUserId) : IRequest<Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>>;

public sealed class GetAllMacropadDeviceByAppUserIdQueryResponse : EntityDto
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }

    public GetMacropadModelDto? MacropadModel { get; set; }
}

internal sealed class GetAllMacropadDeviceByAppUserIdQueryHandler(
    IMacropadDeviceRepository macropadDeviceRepository) : IRequestHandler<GetAllMacropadDeviceByAppUserIdQuery, Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>> Handle(GetAllMacropadDeviceByAppUserIdQuery request, CancellationToken cancellationToken)
    {
        var macropadsFromDb = macropadDeviceRepository
                                .GetAll()
                                .Where(w => w.AppUserId == request.AppUserId)
                                .Include(i => i.MacropadModel)
                                .OrderBy(m => m.CreatedAt)
                                .ToList();

        var macropads = macropadsFromDb.Select(macropad => new GetAllMacropadDeviceByAppUserIdQueryResponse
        {
            Id = macropad.Id,
            MacropadName = macropad.MacropadName,
            MacropadSecretToken = macropad.MacropadSecretToken,
            IsEyeAnimationEnabled = macropad.IsEyeAnimationEnabled,
            MacropadModel = macropad.MacropadModel is not null
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
            CreatedAt = macropad.CreatedAt,
            CreatedBy = macropad.CreatedBy,
        }).ToList();
        return Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>.Succeed(macropads);
    }
}
