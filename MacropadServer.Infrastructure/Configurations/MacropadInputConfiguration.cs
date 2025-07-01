using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacropadServer.Infrastructure.Configurations;
public sealed class MacropadInputConfiguration : IEntityTypeConfiguration<MacropadInput>
{
    public void Configure(EntityTypeBuilder<MacropadInput> builder)
    {
        builder.ToTable("MacropadInputs");

        builder.Property(p => p.InputName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(p => p.InputIndex)
            .IsRequired(false);

        builder.Property(p => p.ModIndex)
            .IsRequired(false);

        builder.Property(p => p.InputBitMap)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(p => p.InputType)
            .HasConversion(p => p.Value,
                           v => InputTypeEnum
                           .FromValue(v));

        builder.Property(p => p.Item1)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(p => p.Item2)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(p => p.Item3)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(p => p.Item4)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasOne(p => p.MacropadDevice)
            .WithMany(p => p.MacropadInputs)
            .HasForeignKey(p => p.MacropadDeviceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
