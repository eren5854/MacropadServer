using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacropadServer.Infrastructure.Configurations;
public sealed class MacropadEyeAnimationConfiguration : IEntityTypeConfiguration<MacropadEyeAnimation>
{
    public void Configure(EntityTypeBuilder<MacropadEyeAnimation> builder)
    {
        builder.ToTable("MacropadEyeAnimations");

        builder.Property(p => p.EyeAnimationType)
            .HasConversion(
                p => p.Value,
                v => EyeAnimationTypeEnum.FromValue(v));

        builder.Property(p => p.EyeAnimationTrigger)
            .HasConversion(
                p => p.Value,
                v => EyeAnimationTriggerEnum.FromValue(v));

        builder.HasOne(p => p.Macropad)
            .WithMany(p => p.MacropadEyeAnimations)
            .HasForeignKey(p => p.MacropadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
