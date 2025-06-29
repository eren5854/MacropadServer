using Ardalis.SmartEnum;

namespace MacropadServer.Domain.Enums;
public sealed class EyeAnimationTriggerEnum : SmartEnum<EyeAnimationTriggerEnum>
{
    public static readonly EyeAnimationTriggerEnum None = new EyeAnimationTriggerEnum("None", 0);
    public static readonly EyeAnimationTriggerEnum Starting = new EyeAnimationTriggerEnum("Starting", 1);
    public static readonly EyeAnimationTriggerEnum ConnectionLost = new EyeAnimationTriggerEnum("ConnectionLost", 2);
    public static readonly EyeAnimationTriggerEnum ConnectionRestored = new EyeAnimationTriggerEnum("ConnectionRestored", 3);
    public static readonly EyeAnimationTriggerEnum ButtonPressed = new EyeAnimationTriggerEnum("ButtonPressed", 4);
    public static readonly EyeAnimationTriggerEnum PullApiData = new EyeAnimationTriggerEnum("PullApiData", 5);
    public EyeAnimationTriggerEnum(string name, int value) : base(name, value)
    {
    }
}
