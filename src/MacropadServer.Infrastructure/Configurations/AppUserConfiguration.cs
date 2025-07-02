using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacropadServer.Infrastructure.Configurations;
public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users");

        builder.HasIndex(i => i.UserName)
            .IsUnique();

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(p => p.UserName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(p => p.Role)
            .HasConversion(p => p.Value,
                           v => UserRoleEnum
                           .FromValue(v));

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
