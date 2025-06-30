using ED.Result;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record ForgotPasswordCommand(
    string Email,
    int ForgotPasswordCode) : IRequest<Result<string>>;

internal sealed class ForgotPasswordCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        DateTime currentDateTime = DateTime.Now;
        if (user.ForgotPasswordCodeSendDate.HasValue && (currentDateTime - user.ForgotPasswordCodeSendDate.Value).TotalHours > 6)
        {
            return Result<string>.Failure("Kodun süresi geçmiş lütfen tekrar deneyiniz");
        }

        if (user.ForgotPasswordCode != request.ForgotPasswordCode)
        {
            return Result<string>.Failure("Kod Hatalı!!");
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }
}
