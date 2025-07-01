using MacropadServer.Application.Auth;
using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;
using MacropadServer.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MacropadServer.Infrastructure.Services;
internal sealed class JwtProvider(
    IOptions<JwtOptions> jwtOptions,
    UserManager<AppUser> userManager) : IJwtProvider
{
    public async Task<LoginCommandResponse> CreateToken(AppUser user)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FullName", user.FullName),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            //new Claim(ClaimTypes.NameIdentifier, user.Email ?? ""),
            new Claim("Email",  user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        DateTime expires = DateTime.UtcNow.AddMonths(6);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        string refreshToken = Guid.NewGuid().ToString();
        DateTime refreshToneExpires = expires;

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshToneExpires;

        await userManager.UpdateAsync(user);

        return new(token, refreshToken, refreshToneExpires);
    }
}
