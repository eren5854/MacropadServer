using ED.GenericRepository;
using ED.Result;
using Google.Apis.Auth;
using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Application.Auth;
public sealed record GoogleLoginCommand(string Id,
    string IdToken,
    string Name,
    string FirstName,
    string LastName,
    string Email,
    string PhotoUrl,
    string Provider) : IRequest<Result<GoogleLoginCommandResponse>>;

public sealed record GoogleLoginCommandResponse(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires);

internal sealed class GoogleLoginCommandHandler(
    UserManager<AppUser> userManager,
    //ILoginLogRepository loginLogRepository,
    //IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider) : IRequestHandler<GoogleLoginCommand, Result<GoogleLoginCommandResponse>>
{
    public async Task<Result<GoogleLoginCommandResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>
            {
                "838164728497-v5vnto6us970bv8kmgfjtjo8r57nl9iv.apps.googleusercontent.com"
            }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

        var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
        AppUser? appUser = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        bool result = appUser != null;
        if (appUser is null)
        {
            appUser = await userManager.FindByEmailAsync(payload.Email);
            if (appUser is null)
            {
                appUser = new()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    //ProfilePicture = request.PhotoUrl,
                    EmailConfirmed = true,
                    CreatedDate = DateTime.UtcNow,
                };
                var identityResult = await userManager.CreateAsync(appUser);
                result = identityResult.Succeeded;
            }
            else
            {
                return Result<GoogleLoginCommandResponse>.Failure("Bu e-posta adresi zaten kayıtlı. Lütfen giriş yapın.");
            }
        }
        if (!result)
        {
            return Result<GoogleLoginCommandResponse>.Failure("Hata!! Kayıt başarısız.");
        }

        await userManager.AddLoginAsync(appUser, info);

        var loginResponse = await jwtProvider.CreateToken(appUser);

        var googleLoginResponse = new GoogleLoginCommandResponse(
            loginResponse.Token,
            loginResponse.RefreshToken,
            loginResponse.RefreshTokenExpires);

        //LoginLog loginLog = new()
        //{
        //    AppUserId = appUser.Id,
        //    LoginDate = DateTime.UtcNow,
        //};

        //loginLogRepository.Add(loginLog);
        //await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GoogleLoginCommandResponse>.Succeed(googleLoginResponse);
    }
}