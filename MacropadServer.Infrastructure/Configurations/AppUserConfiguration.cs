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

        builder
            .Property(p => p.Role)
            .HasConversion(p => p.Value,
                           v => UserRoleEnum
                           .FromValue(v));

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
