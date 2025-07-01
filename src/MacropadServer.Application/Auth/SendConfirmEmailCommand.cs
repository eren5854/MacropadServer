using ED.GenericEmailService;
using ED.GenericEmailService.Models;
using ED.GenericRepository;
using ED.Result;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record SendConfirmEmailCommand(
    string Email) : IRequest<Result<string>>;

internal sealed class SendConfirmEmailCommandHandler(
    UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        if (user.EmailConfirmed)
        {
            return Result<string>.Failure("Mail address already confirmed");
        }

        Random random = new();
        user.EmailConfirmCode = random.Next(100000, 999999);
        await userManager.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        string body = CreateBody(user);
        string subject = "E-Postanı Onayla";

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
        return Result<string>.Succeed("E-posta onay maili gönderildi");
    }

    private string CreateBody(AppUser user)
    {
        string body = $@"Click on the link to confirm your email address.
<a href='http://localhost:4200/confirm-email/{user.Email}/{user.EmailConfirmCode}' target='_blank'>E-postanızı onaylamak için tıklayın.
</a>";
        return body;
    }
}
