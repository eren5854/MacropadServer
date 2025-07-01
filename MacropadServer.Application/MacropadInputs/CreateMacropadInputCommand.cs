using ED.GenericRepository;
using ED.Result;
using FluentValidation;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Application.MacropadInputs;
public sealed record CreateMacropadInputCommand(
    string? InputName,
    int? InputIndex,
    int? ModIndex,
    string? InputBitMap,
    InputTypeEnum InputType,
    string? Item1,
    string? Item2,
    string? Item3,
    string? Item4,
    Guid MacropadId) : IRequest<Result<string>>;

internal sealed class CreateMacropadInputCommandHandler(
    IMacropadInputRepository macropadInputRepository,
    IMacropadRepository macropadRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateMacropadInputCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateMacropadInputCommand request, CancellationToken cancellationToken)
    {
        Macropad? macropad = macropadRepository.Where(w => w.Id == request.MacropadId)
            .Include(i => i.MacropadInputs)
            .Include(i => i.MacropadModel)
            .FirstOrDefault();
        if (macropad is null) return Result<string>.Failure("Macropad bulunamadı");
        if (macropad.MacropadInputs!.Count() >= macropad.MacropadModel!.ButtonCount * macropad.MacropadModel.ModCount)
            return Result<string>.Failure("Macropad giriş sayısı sınırına ulaşıldı");
        if (macropad.MacropadInputs!.Count(x => x.ModIndex == request.ModIndex) >= macropad.MacropadModel!.ButtonCount)
            return Result<string>.Failure("Bu mod için izin verilen maksimum giriş sayısına ulaşıldı");
        if (macropad.MacropadInputs!.Any(x => x.InputIndex == request.InputIndex))
            return Result<string>.Failure("Bu giriş indeksi zaten kullanılıyor");
        MacropadInput macropadInput = request.Adapt<MacropadInput>();
        macropadInputRepository.Add(macropadInput);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad girişi başarıyla oluşturuldu");
    }
}

public sealed class CreateMacropadInputCommandValidator : AbstractValidator<CreateMacropadInputCommand>
{
    public CreateMacropadInputCommandValidator()
    {
        RuleFor(r => r.InputName)
            .NotEmpty().WithMessage("Giriş adı boş olamaz.")
            .MaximumLength(100).WithMessage("Giriş adı en fazla 100 karakter olabilir.");
        RuleFor(r => r.InputIndex)
            .NotEmpty().WithMessage("Giriş indeksi boş olamaz.");
        RuleFor(r => r.ModIndex)
            .NotEmpty().WithMessage("Mod indeksi boş olamaz.");
        RuleFor(r => r.InputType)
            .IsInEnum().WithMessage("Geçersiz giriş tipi.");
        RuleFor(r => r.MacropadId)
            .NotEmpty().WithMessage("Macropad ID boş olamaz.");
        RuleFor(r => r.Item1)
            .MaximumLength(500).WithMessage("Item1 en fazla 500 karakter olabilir.");
        RuleFor(r => r.Item2)
            .MaximumLength(500).WithMessage("Item2 en fazla 500 karakter olabilir.");
        RuleFor(r => r.Item3)
            .MaximumLength(500).WithMessage("Item3 en fazla 500 karakter olabilir.");
        RuleFor(r => r.Item4)
            .MaximumLength(500).WithMessage("Item4 en fazla 500 karakter olabilir.");
    }
}
