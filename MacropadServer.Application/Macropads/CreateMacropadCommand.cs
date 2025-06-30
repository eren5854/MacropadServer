using ED.GenericRepository;
using ED.Result;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using Mapster;
using MediatR;

namespace MacropadServer.Application.Macropads;
public sealed record CreateMacropadCommand(
    string MacropadName,
    bool? IsEyeAnimationEnabled,
    Guid MacropadModelId,
    Guid AppUserId) : IRequest<Result<string>>;

internal sealed class CreateMacropadCommandHandler(
    IMacropadRepository macropadRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateMacropadCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateMacropadCommand request, CancellationToken cancellationToken)
    {
        Macropad macropad = request.Adapt<Macropad>();
        macropad.MacropadSecretToken = "create generate secret token metod";

        macropadRepository.Add(macropad);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Macropad kaydı başarılı");

    }
}
