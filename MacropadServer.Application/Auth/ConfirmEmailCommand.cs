using ED.Result;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record ConfirmEmailCommand(string Email, int EmailConfirmCode) : IRequest<Result<string>>;

internal sealed record ConfirmEmailCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure("E-posta adresi zaten onaylı");
        }

        if (user.EmailConfirmCode != request.EmailConfirmCode)
        {
            return Result<string>.Failure("E-posta onay kodu yanlış");
        }

        user.EmailConfirmed = true;
        await userManager.UpdateAsync(user);

        return Result<string>.Succeed("E-posta adresi onaylandı");
    }
}