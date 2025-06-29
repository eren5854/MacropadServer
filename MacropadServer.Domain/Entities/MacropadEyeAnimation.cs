using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Enums;

namespace MacropadServer.Domain.Entities;
public sealed class MacropadEyeAnimation : Entity
{
    public EyeAnimationTypeEnum EyeAnimationType { get; set; } = EyeAnimationTypeEnum.None;
    public EyeAnimationTriggerEnum EyeAnimationTrigger { get; set; } = EyeAnimationTriggerEnum.None;

    public Guid MacropadId { get; set; }
    public Macropad Macropad { get; set; } = default!;
}
