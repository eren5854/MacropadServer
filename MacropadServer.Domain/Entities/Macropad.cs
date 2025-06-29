using MacropadServer.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace MacropadServer.Domain.Entities;
public sealed class Macropad : Entity
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSerialNo { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;

    public bool? IsEyeAnimationEnabled { get; set; }

    [JsonIgnore]
    public Guid? MacropadModelId { get; set; }
    [JsonIgnore]
    public MacropadModel? MacropadModel { get; set; }

    public IEnumerable<MacropadInput>? MacropadInputs { get; set; }
    public IEnumerable<MacropadEyeAnimation>? MacropadEyeAnimations { get; set; }
}
