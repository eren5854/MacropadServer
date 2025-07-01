using MacropadServer.Domain.Entities;

namespace MacropadServer.Application.Services;
public interface IGenerate
{
    Task<string> GenerateSerialNumber(MacropadModel macropadModel);
    Task<string> GenerateSecretToken();
    public void GenerateMacropadInput(MacropadDevice macropad);
}
