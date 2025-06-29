using Ardalis.SmartEnum;

namespace MacropadServer.Domain.Enums;
public sealed class MacropadConnectionTypeEnum : SmartEnum<MacropadConnectionTypeEnum>
{
    public static readonly MacropadConnectionTypeEnum USB = new MacropadConnectionTypeEnum("USB", 1);
    public static readonly MacropadConnectionTypeEnum Bluetooth = new MacropadConnectionTypeEnum("Bluetooth", 2);
    public static readonly MacropadConnectionTypeEnum WiFi = new MacropadConnectionTypeEnum("WiFi", 3);
    public static readonly MacropadConnectionTypeEnum Ethernet = new MacropadConnectionTypeEnum("2.4GHz", 4);

    public MacropadConnectionTypeEnum(string name, int value) : base(name, value)
    {
    }
}
