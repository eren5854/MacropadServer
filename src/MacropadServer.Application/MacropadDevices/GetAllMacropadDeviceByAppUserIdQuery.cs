using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadDevices;
public sealed record GetAllMacropadDeviceByAppUserIdQuery(Guid AppUserId) : IRequest<Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>>;

public sealed class GetAllMacropadDeviceByAppUserIdQueryResponse : EntityDto
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }

    public MacropadModel? MacropadModel { get; set; }
    public IEnumerable<MacropadInput>? MacropadInputs { get; set; }
    public IEnumerable<MacropadEyeAnimation>? MacropadEyeAnimations { get; set; }

}

internal sealed class GetAllMacropadDeviceByAppUserIdQueryHandler(
    IMacropadDeviceRepository macropadDeviceRepository) : IRequestHandler<GetAllMacropadDeviceByAppUserIdQuery, Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>> Handle(GetAllMacropadDeviceByAppUserIdQuery request, CancellationToken cancellationToken)
    {
        var macropadDevices = macropadDeviceRepository.GetAll()
            .Where(macropad => macropad.AppUserId == request.AppUserId)
            .Select(macropad => new GetAllMacropadDeviceByAppUserIdQueryResponse
            {
                Id = macropad.Id,
                MacropadName = macropad.MacropadName,
                MacropadSecretToken = macropad.MacropadSecretToken,
                IsEyeAnimationEnabled = macropad.IsEyeAnimationEnabled,
                MacropadModel = macropad.MacropadModel,
                MacropadInputs = macropad.MacropadInputs,
                //MacropadEyeAnimations = macropad.MacropadEyeAnimations
            })
            .AsQueryable();
        //if (!macropadDevices.Any()) return Result<IQueryable<GetAllMacropadDeviceByAppUserIdQueryResponse>>.Failure("Macropad cihazları bulunamadı");
        return Result<IEnumerable<GetAllMacropadDeviceByAppUserIdQueryResponse>>.Succeed(macropadDevices);
    }
}
