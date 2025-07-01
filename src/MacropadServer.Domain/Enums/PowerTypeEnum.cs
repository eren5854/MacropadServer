using Ardalis.SmartEnum;

namespace MacropadServer.Domain.Enums;
public sealed class PowerTypeEnum : SmartEnum<PowerTypeEnum>
{
    public static readonly PowerTypeEnum USB = new PowerTypeEnum("USB Adapter", 1);
    public static readonly PowerTypeEnum Battery = new PowerTypeEnum("Battery", 2);
    public static readonly PowerTypeEnum AC = new PowerTypeEnum("AC", 3);
    public static readonly PowerTypeEnum Solar = new PowerTypeEnum("Solar", 4);
    // public static readonly PowerTypeEnum Other = new PowerTypeEnum("Other", 5);
    public PowerTypeEnum(string name, int value) : base(name, value)
    {
    }
}
