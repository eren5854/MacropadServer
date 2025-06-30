using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Enums;
using System.Text.Json.Serialization;

namespace MacropadServer.Domain.Entities;
public sealed class MacropadEyeAnimation : Entity
{
    public EyeAnimationTypeEnum EyeAnimationType { get; set; } = EyeAnimationTypeEnum.None;
    public EyeAnimationTriggerEnum EyeAnimationTrigger { get; set; } = EyeAnimationTriggerEnum.None;

    [JsonIgnore]
    public Guid MacropadId { get; set; }
    [JsonIgnore]
    public Macropad Macropad { get; set; } = default!;
}
