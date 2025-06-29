using Ardalis.SmartEnum;

namespace MacropadServer.Domain.Enums;
public sealed class InputTypeEnum : SmartEnum<InputTypeEnum>
{
    public static readonly InputTypeEnum Keyboard = new InputTypeEnum("Keyboard", 1);
    public static readonly InputTypeEnum Encoder = new InputTypeEnum("Encoder", 2);
    public static readonly InputTypeEnum Gamepad = new InputTypeEnum("Gamepad", 3);
    public static readonly InputTypeEnum IoT = new InputTypeEnum("IoT", 4);

    public InputTypeEnum(string name, int value) : base(name, value)
    {
    }
}
