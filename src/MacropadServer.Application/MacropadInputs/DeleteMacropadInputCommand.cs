using ED.GenericRepository;
using ED.Result;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MediatR;

namespace MacropadServer.Application.MacropadInputs;
public sealed record DeleteMacropadInputCommand(
    Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteMacropadInputCommandHandler(
    IMacropadInputRepository macropadInputRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteMacropadInputCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteMacropadInputCommand request, CancellationToken cancellationToken)
    {
        MacropadInput macropadInput = macropadInputRepository.GetByExpression(g => g.Id == request.Id);
        if (macropadInput is null) return Result<string>.Failure("Macropad girişi bulunamadı");
        macropadInputRepository.Delete(macropadInput);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad girişi başarıyla silindi");
    }
}
