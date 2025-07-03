using ED.GenericRepository;
using ED.Result;
using FluentValidation;
using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;

namespace MacropadServer.Application.MacropadDevices;
public sealed record CreateMacropadDeviceCommand(
    string MacropadName,
    bool? IsEyeAnimationEnabled,
    string ModelSerialNo,
    Guid AppUserId) : IRequest<Result<string>>;

internal sealed class CreateMacropadDeviceCommandHandler(
    IMacropadDeviceRepository macropadRepository,
    IMacropadModelRepository macropadModelRepository,
    IAppUserRepository appUserRepository,
    IUnitOfWork unitOfWork,
    IGenerate generate) : IRequestHandler<CreateMacropadDeviceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateMacropadDeviceCommand request, CancellationToken cancellationToken)
    {
        MacropadModel macropadModel = macropadModelRepository.GetByExpression(
            g => g.ModelSerialNo == request.ModelSerialNo);
        if (macropadModel is null) return Result<string>.Failure("Macropad modeli bulunamadı");
        bool IsAppUserExist = await appUserRepository.AnyAsync(
            x => x.Id == request.AppUserId, cancellationToken);
        if (!IsAppUserExist) return Result<string>.Failure("Kullanıcı bulunamadı");
        MacropadDevice macropad = request.Adapt<MacropadDevice>();
        macropad.MacropadModelId = macropadModel.Id;
        macropad.MacropadSecretToken = await generate.GenerateSecretToken();
        macropadRepository.Add(macropad);
        generate.GenerateMacropadInput(macropad, macropadModel);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad kaydı başarılı");
    }
}

public sealed class CreateMacropadDeviceCommandValidator : AbstractValidator<CreateMacropadDeviceCommand>
{
    public CreateMacropadDeviceCommandValidator()
    {
        RuleFor(r => r.MacropadName)
            .NotEmpty().WithMessage("Macropad adı boş olamaz.")
            .MaximumLength(100).WithMessage("Macropad adı en fazla 100 karakter olabilir.");
        RuleFor(r => r.ModelSerialNo)
            .NotEmpty().WithMessage("Model boş olamaz.");
        RuleFor(r => r.AppUserId)
            .NotEmpty().WithMessage("Kullanıcı boş olamaz.");
    }
}