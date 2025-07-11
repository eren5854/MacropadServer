﻿using ED.GenericRepository;
using ED.Result;
using FluentValidation;
using GenericFileService.Files;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MacropadServer.Application.MacropadModels;
public sealed class UpdateMacropadModelCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public string? ModelVersion { get; set; }
    public string? ModelDescription { get; set; }
    public IFormFile? ModelImage { get; set; }
    public string? DeviceSupport { get; set; }
    public int ButtonCount { get; set; }
    public int ModCount { get; set; }
    public bool IsScreenExist { get; set; }
    public string? ScreenType { get; set; }
    public double? ScreenSize { get; set; }
    public MacropadConnectionTypeEnum? ConnectionType { get; set; }
    public string? MicrocontrollerType { get; set; }
    public PowerTypeEnum? PowerType { get; set; }
    public bool? Rechargeable { get; set; }
    public string? CaseColor { get; set; }
    public string? CaseMaterial { get; set; }
    public string? CaseDescription { get; set; }
}
    

internal sealed class UpdateMacropadModelCommandHadnler(
    IMacropadModelRepository macropadModelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateMacropadModelCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMacropadModelCommand request, CancellationToken cancellationToken)
    {
        MacropadModel macropadModel = macropadModelRepository.GetByExpression(g => g.Id == request.Id);
        if(macropadModel is null) return Result<string>.Failure("Macropad modeli bulunamadı.");
        bool isModelNameExists = macropadModelRepository.Any(a => a.ModelName == request.ModelName && a.Id != request.Id);
        if (isModelNameExists) return Result<string>.Failure("Model ismi zaten mevcut");
        string? image = null;
        if (request.ModelImage is not null)
        {
            image = FileService.FileSaveToServer(request.ModelImage, "wwwroot/MacropadModelImages/");
            FileService.FileDeleteToServer($"wwwroot/MacropadModelImages/{macropadModel.ModelImage}");
        }
        else image = macropadModel.ModelImage;
        macropadModel = request.Adapt(macropadModel);
        macropadModel.ModelImage = image;
        macropadModelRepository.Update(macropadModel);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad modeli başarıyla güncellendi.");
    }
}

public sealed class UpdateMacropadModelCommandValidator : AbstractValidator<UpdateMacropadModelCommand>
{
    public UpdateMacropadModelCommandValidator()
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
