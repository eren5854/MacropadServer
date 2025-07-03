using MacropadServer.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace MacropadServer.Domain.Entities;
public sealed class MacropadDevice : Entity
{
    public string MacropadName { get; set; } = string.Empty;
    public string MacropadSecretToken { get; set; } = string.Empty;
    public bool? IsEyeAnimationEnabled { get; set; }

    [JsonIgnore]
    public Guid? MacropadModelId { get; set; }
    [JsonIgnore]
    public MacropadModel? MacropadModel { get; set; }

    [JsonIgnore]
    public Guid AppUserId { get; set; }
    [JsonIgnore]
    public AppUser AppUser { get; set; } = default!;

    public List<MacropadInput>? MacropadInputs { get; set; }
    public List<MacropadEyeAnimation>? MacropadEyeAnimations { get; set; }
}
