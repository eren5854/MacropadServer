using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.Macropads;
public sealed record GetAllMacropadQuery() : IRequest<Result<IQueryable<GetAllMacropadQueryResponse>>>;

public sealed class GetAllMacropadQueryResponse : EntityDto
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }

    public IEnumerable<MacropadInput>? MacropadInputs { get; set; }
    public IEnumerable<MacropadEyeAnimation>? MacropadEyeAnimations { get; set; }
}

internal sealed class GetAllMacropadQueryHandler(
    IMacropadRepository macropadRepository) : IRequestHandler<GetAllMacropadQuery, Result<IQueryable<GetAllMacropadQueryResponse>>>
{
    public async Task<Result<IQueryable<GetAllMacropadQueryResponse>>> Handle(GetAllMacropadQuery request, CancellationToken cancellationToken)
    {
        var macropads = macropadRepository.GetAll()
            .Select(macropad => new GetAllMacropadQueryResponse
            {
                Id = macropad.Id,
                MacropadName = macropad.MacropadName,
                MacropadSecretToken = macropad.MacropadSecretToken,
                IsEyeAnimationEnabled = macropad.IsEyeAnimationEnabled,
                MacropadInputs = macropad.MacropadInputs,
                MacropadEyeAnimations = macropad.MacropadEyeAnimations
            })
            .AsQueryable();
        return Result<IQueryable<GetAllMacropadQueryResponse>>.Succeed(macropads);
    }
}
