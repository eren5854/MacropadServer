using ED.GenericRepository;
using ED.Result;
using FluentValidation;
using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;

namespace MacropadServer.Application.MacropadModels;
public sealed record CreateMacropadModelCommand(
    string ModelName,
    string? ModelVersion,
    string? ModelDescription,
    string? DeviceSupport,
    int ButtonCount,
    int ModCount,
    bool IsScreenExist,
    string? ScreenType,
    double? ScreenSize,
    MacropadConnectionTypeEnum? ConnectionType,
    string? MicrocontrollerType,
    PowerTypeEnum? PowerType,
    bool? Rechargeable,
    string? CaseColor,
    string? CaseMaterial,
    string? CaseDescription
) : IRequest<Result<string>>;

internal sealed class CreateMacropadModelCommandHandler(
    IMacropadModelRepository macropadModelRepository,
    IGenerate generate,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateMacropadModelCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateMacropadModelCommand request, CancellationToken cancellationToken)
    {
        bool isModelNameExists = macropadModelRepository.Any(a => a.ModelName == request.ModelName);
        if (isModelNameExists) return Result<string>.Failure("Model ismi zaten mevcut");
        MacropadModel macropadModel = request.Adapt<MacropadModel>();
        macropadModel.ModelSerialNo = await generate.GenerateSerialNumber(macropadModel);
        macropadModelRepository.Add(macropadModel);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad modeli başarıyla oluşturuldu");
    }
}

public sealed class CreateMacropadModelCommandValidator : AbstractValidator<CreateMacropadModelCommand>
{
    public CreateMacropadModelCommandValidator()
    {
        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage("Model adı boş olamaz")
            .MaximumLength(100).WithMessage("Model adı en fazla 100 karakter olabilir");
        RuleFor(x => x.ModelVersion)
            .MaximumLength(50).WithMessage("Model versiyonu en fazla 50 karakter olabilir");
        RuleFor(x => x.ModelDescription)
            .MaximumLength(500).WithMessage("Model açıklaması en fazla 500 karakter olabilir");
        RuleFor(x => x.DeviceSupport)
            .MaximumLength(20).WithMessage("Cihaz desteği en fazla 200 karakter olabilir");
        RuleFor(x => x.ScreenType)
            .MaximumLength(50).WithMessage("Ekran tipi en fazla 50 karakter olabilir");
        RuleFor(x => x.MicrocontrollerType)
            .MaximumLength(50).WithMessage("Mikrodenetleyici tipi en fazla 50 karakter olabilir");
        RuleFor(x => x.CaseColor)
            .MaximumLength(50).WithMessage("Kasa rengi en fazla 50 karakter olabilir");
        RuleFor(x => x.CaseMaterial)
            .MaximumLength(50).WithMessage("Kasa malzemesi en fazla 50 karakter olabilir");
        RuleFor(x => x.CaseDescription)
            .MaximumLength(500).WithMessage("Kasa açıklaması en fazla 500 karakter olabilir");
    }
}
