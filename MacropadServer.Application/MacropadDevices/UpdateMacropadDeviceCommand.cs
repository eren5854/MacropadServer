using ED.GenericRepository;
using ED.Result;
using FluentValidation;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;

namespace MacropadServer.Application.MacropadDevices;
public sealed record UpdateMacropadDeviceCommand(
    Guid Id,
    string MacropadName,
    bool? IsEyeAnimationEnabled,
    Guid MacropadModelId,
    Guid AppUserId) : IRequest<Result<string>>;

internal sealed class UpdateMacropadDeviceCommandHandler(
    IMacropadDeviceRepository macropadDeviceRepository,
    IMacropadModelRepository macropadModelRepository,
    IAppUserRepository appUserRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateMacropadDeviceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMacropadDeviceCommand request, CancellationToken cancellationToken)
    {
        MacropadDevice macropadDevice = macropadDeviceRepository
            .GetByExpression(g => g.Id == request.Id);
        if (macropadDevice is null) return Result<string>.Failure("Macropad cihazı bulunamadı");
        bool IsMacropadModelExist = await macropadModelRepository
            .AnyAsync(x => x.Id == request.MacropadModelId, cancellationToken);
        if (!IsMacropadModelExist) return Result<string>.Failure("Macropad modeli bulunamadı");
        bool IsAppUserExist = await appUserRepository
            .AnyAsync(x => x.Id == request.AppUserId, cancellationToken);
        if (!IsAppUserExist) return Result<string>.Failure("Kullanıcı bulunamadı");
        request.Adapt(macropadDevice);
        macropadDevice.UpdatedAt = DateTimeOffset.UtcNow;
        macropadDeviceRepository.Update(macropadDevice);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad cihazı güncellendi");
    }
}

public sealed class UpdateMacropadDeviceCommandValidator : AbstractValidator<UpdateMacropadDeviceCommand>
{
    public UpdateMacropadDeviceCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Macropad cihazı ID boş olamaz.");
        RuleFor(r => r.MacropadName)
            .NotEmpty().WithMessage("Macropad adı boş olamaz.")
            .MaximumLength(100).WithMessage("Macropad adı en fazla 100 karakter olabilir.");
        RuleFor(r => r.MacropadModelId)
            .NotEmpty().WithMessage("Macropad modeli boş olamaz.");
        RuleFor(r => r.AppUserId)
            .NotEmpty().WithMessage("Kullanıcı boş olamaz.");
    }
}