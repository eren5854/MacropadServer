using MacropadServer.Domain.Entities;

namespace MacropadServer.Application.Services;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateToken(AppUser user);
}
