using MacropadServer.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Domain.Entities;
public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);

    public string SecretToken { get; set; } = default!;

    public IEnumerable<MacropadDevice>? Macropads { get; set; }

    public UserRoleEnum Role { get; set; } = UserRoleEnum.User;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }

    public int? ForgotPasswordCode { get; set; }
    public DateTime? ForgotPasswordCodeSendDate { get; set; }

    public int? EmailConfirmCode { get; set; }

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
