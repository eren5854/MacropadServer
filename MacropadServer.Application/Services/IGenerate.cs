using MacropadServer.Domain.Entities;

namespace MacropadServer.Application.Services;
public interface IGenerate
{
    Task<string> GenerateSerialNumber(Macropad macropad);

    Task<string> GenerateSecretToken(Macropad macropad);
}
