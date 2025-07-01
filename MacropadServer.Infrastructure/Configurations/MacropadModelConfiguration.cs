using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacropadServer.Infrastructure.Configurations;
public sealed class MacropadModelConfiguration : IEntityTypeConfiguration<MacropadModel>
{
    public void Configure(EntityTypeBuilder<MacropadModel> builder)
    {
        builder.ToTable("MacropadModels");

        builder.Property(p => p.ModelName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ModelSerialNo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.ModelVersion)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(p => p.ModelDescription)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(p => p.DeviceSupport)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(p => p.ButtonCount)
            .IsRequired();

        builder.Property(p => p.ModCount)
            .IsRequired();

        builder.Property(p => p.IsScreenExist)
            .IsRequired();

        builder.Property(p => p.ScreenType)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.ScreenSize)
            .IsRequired(false);

        builder
           .Property(p => p.ConnectionType)
           .HasConversion(p => p.Value,
                          v => MacropadConnectionTypeEnum
                          .FromValue(v));

        builder.Property(p => p.MicrocontrollerType)
            .HasMaxLength(50)
            .IsRequired(false);

        builder
          .Property(p => p.PowerType)
          .HasConversion(p => p.Value,
                         v => PowerTypeEnum
                         .FromValue(v));

        builder.Property(p => p.Rechargeable)
            .IsRequired(false);

        builder.Property(p => p.CaseColor)
            .HasMaxLength(50);

        builder.Property(p => p.CaseMaterial)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(p => p.CaseDescription)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
