using MacropadServer.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MacropadServer.Domain.Entities;
public sealed class AppUser : IdentityUser<Guid>
{
    public AppUser()
    {
        Id = Guid.CreateVersion7(); // Use a version 7 GUID for better sorting
        CreatedAt = DateTimeOffset.UtcNow;
        IsDeleted = false;
        IsActived = true;
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName); //computed property

    public string SecretToken { get; set; } = default!;

    public IEnumerable<MacropadDevice>? Macropads { get; set; }

    public UserRoleEnum Role { get; set; } = UserRoleEnum.User;

    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpires { get; set; }

    public int? ForgotPasswordCode { get; set; }
    public DateTimeOffset? ForgotPasswordCodeSendDate { get; set; }

    public int? EmailConfirmCode { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsActived { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
    public string? DeleteBy{ get; set; }
}
