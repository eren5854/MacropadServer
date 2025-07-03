using ED.GenericRepository;
using ED.Result;
using FluentValidation;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;

namespace MacropadServer.Application.MacropadInputs;
public sealed record UpdateMacropadInputCommand(
    Guid Id,
    string? InputName,
    string? InputBitMap,
    InputTypeEnum InputType,
    string? Item1,
    string? Item2,
    string? Item3,
    string? Item4) : IRequest<Result<string>>;

internal sealed class UpdateMacropadInputCommandHandler(
    IMacropadInputRepository macropadInputRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateMacropadInputCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMacropadInputCommand request, CancellationToken cancellationToken)
    {
        MacropadInput? macropadInput = macropadInputRepository.GetByExpression(g => g.Id == request.Id);
        if (macropadInput is null) return Result<string>.Failure("Macropad girişi bulunamadı");
        request.Adapt(macropadInput);
        macropadInputRepository.Update(macropadInput);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad girişi başarıyla güncellendi");
    }
}

public sealed class UpdateMacropadInputCommandValidator : AbstractValidator<UpdateMacropadInputCommand>
{
    public UpdateMacropadInputCommandValidator()
    {
        RuleFor(r => r.InputName)
            .NotEmpty().WithMessage("Giriş adı boş olamaz.")
            .MaximumLength(100).WithMessage("Giriş adı en fazla 100 karakter olabilir.");
        RuleFor(r => r.InputType)
            .IsInEnum().WithMessage("Geçersiz giriş tipi.");
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