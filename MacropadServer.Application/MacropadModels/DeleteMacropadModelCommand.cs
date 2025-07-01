using ED.GenericRepository;
using ED.Result;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadModels;
public sealed record DeleteMacropadModelCommand(
    Guid MacropadModelId) : IRequest<Result<string>>;

internal sealed class DeleteMacropadModelCommandHandler(
    IMacropadModelRepository macropadModelRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteMacropadModelCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteMacropadModelCommand request, CancellationToken cancellationToken)
    {
        MacropadModel macropadModel = macropadModelRepository.GetByExpression(g => g.Id == request.MacropadModelId);
        if (macropadModel is null) return Result<string>.Failure("Macropad model bulunamadı.");
        macropadModel.IsDeleted = true;
        macropadModelRepository.Update(macropadModel);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad modeli başarıyla silindi.");
    }
}
