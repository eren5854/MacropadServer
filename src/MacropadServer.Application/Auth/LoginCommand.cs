using ED.Result;
using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Application.Auth;
public sealed record LoginCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<LoginCommandResponse>>;

public sealed record LoginCommandResponse(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires);

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        string emailOrUsername = request.EmailOrUserName;
        AppUser? appUser = await userManager
            .Users
            .FirstOrDefaultAsync(p => p.Email == emailOrUsername ||
                                    p.UserName == emailOrUsername, cancellationToken);
        if (appUser is null) 
            return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadı");
        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(appUser, request.Password, true);
        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = appUser.LockoutEnd - DateTimeOffset.UtcNow;
            if (timeSpan is not null) 
                return Result<LoginCommandResponse>.Failure($"Password entered incorrectly 3 times! Wait {Math.Ceiling(timeSpan.Value.TotalSeconds)} seconds.");
            else 
                return Result<LoginCommandResponse>.Failure("Wait 3 minutes");
        }
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed) 
                return Result<LoginCommandResponse>.Failure("E-mail adresi onaylı değil");
            return Result<LoginCommandResponse>.Failure("Şifre Yanlış");
        }
        var loginResponse = await jwtProvider.CreateToken(appUser);
        //LoginLog loginLog = new()
        //{
        //    AppUserId = appUser.Id,
        //    LoginDate = DateTime.UtcNow,
        //};
        //loginLogRepository.Add(loginLog);
        //await unitOfWork.SaveChangesAsync(cancellationToken);
        return loginResponse;
    }
}
