using Ardalis.SmartEnum;

namespace MacropadServer.Domain.Enums;
public sealed class UserRoleEnum : SmartEnum<UserRoleEnum>
{
    public static readonly UserRoleEnum Admin = new UserRoleEnum("Admin", 1);
    public static readonly UserRoleEnum User = new UserRoleEnum("User", 2);

    public UserRoleEnum(string name, int value) : base(name, value)
    {
    }
}
