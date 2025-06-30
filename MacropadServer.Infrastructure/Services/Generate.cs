using MacropadServer.Application.Services;
using MacropadServer.Domain.Entities;

namespace MacropadServer.Infrastructure.Services;
internal sealed class Generate : IGenerate
{
    public Task<string> GenerateSecretToken(Macropad macropad)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateSerialNumber(Macropad macropad)
    {
        throw new NotImplementedException();
    }
}
