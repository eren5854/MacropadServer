using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadDevices;
public sealed record GetAllMacropadDeviceQuery() : IRequest<Result<IQueryable<GetAllMacropadDeviceQueryResponse>>>;

public sealed class GetAllMacropadDeviceQueryResponse : EntityDto
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }

    public IEnumerable<MacropadInput>? MacropadInputs { get; set; }
    public IEnumerable<MacropadEyeAnimation>? MacropadEyeAnimations { get; set; }
}

internal sealed class GetAllMacropadDeviceQueryHandler(
    IMacropadDeviceRepository macropadRepository) : IRequestHandler<GetAllMacropadDeviceQuery, Result<IQueryable<GetAllMacropadDeviceQueryResponse>>>
{
    public async Task<Result<IQueryable<GetAllMacropadDeviceQueryResponse>>> Handle(GetAllMacropadDeviceQuery request, CancellationToken cancellationToken)
    {
        var macropads = macropadRepository.GetAll()
            .Select(macropad => new GetAllMacropadDeviceQueryResponse
            {
                Id = macropad.Id,
                MacropadName = macropad.MacropadName,
                MacropadSecretToken = macropad.MacropadSecretToken,
                IsEyeAnimationEnabled = macropad.IsEyeAnimationEnabled,
                MacropadInputs = macropad.MacropadInputs,
                //MacropadEyeAnimations = macropad.MacropadEyeAnimations
            })
            .AsQueryable();
        return Result<IQueryable<GetAllMacropadDeviceQueryResponse>>.Succeed(macropads);
    }
}
