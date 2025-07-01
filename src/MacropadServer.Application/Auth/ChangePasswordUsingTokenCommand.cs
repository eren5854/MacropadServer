using ED.Result;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record ChangePasswordUsingTokenCommand(
    string Email,
    string NewPassword,
    string Token) : IRequest<Result<string>>;

internal sealed class ChangePasswordUsingTokenCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ChangePasswordUsingTokenCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordUsingTokenCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        IdentityResult result = await userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Hata!!");
        }

        return Result<string>.Succeed("Yeni şifre oluşturuldu");
    }
}