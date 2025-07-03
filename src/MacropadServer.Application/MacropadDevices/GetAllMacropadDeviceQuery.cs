using ED.Result;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadDevices;
public sealed record GetAllMacropadDeviceQuery() : IRequest<Result<IEnumerable<GetAllMacropadDeviceQueryResponse>>>;

public sealed class GetAllMacropadDeviceQueryResponse : EntityDto
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }
}

internal sealed class GetAllMacropadDeviceQueryHandler(
    IMacropadDeviceRepository macropadDeviceRepository) : IRequestHandler<GetAllMacropadDeviceQuery, Result<IEnumerable<GetAllMacropadDeviceQueryResponse>>>
{
    public async Task<Result<IEnumerable<GetAllMacropadDeviceQueryResponse>>> Handle(GetAllMacropadDeviceQuery request, CancellationToken cancellationToken)
    {
        var macropads = macropadDeviceRepository
                                .GetAll()
                                .OrderByDescending(m => m.CreatedAt)
                                .Select(s => new GetAllMacropadDeviceQueryResponse
                                {
                                    Id = s.Id,
                                    MacropadName = s.MacropadName,
                                    MacropadSecretToken = s.MacropadSecretToken,
                                    IsEyeAnimationEnabled = s.IsEyeAnimationEnabled,
                                    CreatedAt = s.CreatedAt,
                                    CreatedBy = s.CreatedBy,
                                    UpdatedAt = s.UpdatedAt,
                                    UpdatedBy = s.UpdatedBy,
                                    DeleteAt = s.DeleteAt,
                                    DeleteBy = s.DeleteBy,
                                    IsActived = s.IsActived
                                })
                                .ToList();
        return Result<IEnumerable<GetAllMacropadDeviceQueryResponse>>.Succeed(macropads);
    }
}
