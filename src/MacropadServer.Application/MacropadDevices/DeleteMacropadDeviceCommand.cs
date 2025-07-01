using ED.GenericRepository;
using ED.Result;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadDevices;
public sealed record DeleteMacropadDeviceCommand(
    Guid MacropadDeviceId,
    Guid AppUserId) : IRequest<Result<string>>;

internal sealed class DeleteMacropadDeviceCommandHandler(
    IMacropadDeviceRepository macropadDeviceRepository,
    IAppUserRepository appUserRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteMacropadDeviceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteMacropadDeviceCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = appUserRepository.GetByExpression(g => g.Id == request.AppUserId);
        if (appUser is null) return Result<string>.Failure("Kullanıcı bulunamadı.");
        MacropadDevice macropadDevice = macropadDeviceRepository.GetByExpression(g => g.Id == request.MacropadDeviceId);
        if (macropadDevice is null) return Result<string>.Failure("Makropad cihazı bulunamadı.");
        if (macropadDevice.AppUserId != request.AppUserId) return Result<string>.Failure("Bu makropad cihazına erişiminiz yok.");
        macropadDevice.IsDeleted = true;
        macropadDeviceRepository.Update(macropadDevice);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Makropad cihazı başarıyla silindi.");
    }
}