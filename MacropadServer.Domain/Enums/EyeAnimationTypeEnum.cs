using Ardalis.SmartEnum;

namespace MacropadServer.Domain.Enums;
public sealed class EyeAnimationTypeEnum : SmartEnum<EyeAnimationTypeEnum>
{
    public static readonly EyeAnimationTypeEnum None = new EyeAnimationTypeEnum("None", 0);
    public static readonly EyeAnimationTypeEnum Default = new EyeAnimationTypeEnum("Default", 1);
    public static readonly EyeAnimationTypeEnum Tired = new EyeAnimationTypeEnum("Tired", 2);
    public static readonly EyeAnimationTypeEnum Angry = new EyeAnimationTypeEnum("Angry", 3);
    public static readonly EyeAnimationTypeEnum Happy = new EyeAnimationTypeEnum("Happy", 4);


    public EyeAnimationTypeEnum(string name, int value) : base(name, value)
    {
    }
}
