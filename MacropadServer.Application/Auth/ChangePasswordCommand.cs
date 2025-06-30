using ED.Result;
using FluentValidation;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record ChangePasswordCommand(
    Guid AppUserId,
    string CurrentPassword,
    string NewPassword) : IRequest<Result<string>>;

internal sealed class ChangePasswordCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ChangePasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(request.AppUserId.ToString());
        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı");
        }

        if (request.CurrentPassword == request.NewPassword)
        {
            return Result<string>.Failure("Yeni şifre mevcut şifreden farklı olmalı");
        }

        IdentityResult result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Hata!! Şifre değiştirme başarısız!!");
        }

        return Result<string>.Succeed("Şifre değiştirme başarılı.");
    }
}

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(r => r.NewPassword)
            .NotEmpty()
            .WithMessage("New password is required.")
            .MinimumLength(8)
            .WithMessage("New password must be at least 8 characters long.")
            .Matches("[A-Z]")
            .WithMessage("New password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("New password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("New password must contain at least one number.");
        //.Matches("[^a-zA-Z0-9]")
        //.WithMessage("New password must contain at least one special character.");
    }
}
