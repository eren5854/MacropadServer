using ED.GenericEmailService;
using ED.GenericEmailService.Models;
using ED.GenericRepository;
using ED.Result;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record SendForgotPasswordEmailCommand(
    string Email) : IRequest<Result<string>>;

internal sealed class SendForgotPasswordEmailCommandHandler(
    UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork) : IRequestHandler<SendForgotPasswordEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendForgotPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı Bulunamadı");
        }

        Random random = new();
        user.ForgotPasswordCode = random.Next(100000, 999999);  // 6 haneli kod oluştur
        user.ForgotPasswordCodeSendDate = DateTimeOffset.UtcNow;

        await userManager.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        string body = CreateBody(user.ForgotPasswordCode, user.Email);
        string subject = "Şifremi Unuttum";

        EmailConfigurations emailConfigurations = new(
           "smtp.gmail.com",
           "yqijsgncjxbmrtts",
           465,
           true,
           true);

        EmailModel<Stream> emailModel = new(
            emailConfigurations,
            "cvturkiye54@gmail.com",
            new List<string> { user.Email ?? "" },
            subject,
            body,
            null);

        await EmailService.SendEmailWithMailKitAsync(emailModel);

        return Result<string>.Succeed("Şifremi unuttum maili gönderildi");
    }

    private string CreateBody(int? code, string? email)
    {
        string body = $@"Şifrenizi değiştirmek için aşağıdaki linke tıklayın: 
<a href='https://localhost:4200/forgot-password/{email}/{code}' target='_blank'>https://localhost:4200/forgot-password/{email}/{code}
</a>";
        return body;
    }
}
