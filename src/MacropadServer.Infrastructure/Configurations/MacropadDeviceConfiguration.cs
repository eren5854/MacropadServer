﻿using MacropadServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacropadServer.Infrastructure.Configurations;
public sealed class MacropadDeviceConfiguration : IEntityTypeConfiguration<MacropadDevice>
{
    public void Configure(EntityTypeBuilder<MacropadDevice> builder)
    {
        builder.ToTable("MacropadDevices");

        builder.Property(p => p.MacropadName)
            .IsRequired()
            .HasMaxLength(100);

        //builder.Property(p => p.MacropadSerialNo)
        //    .IsRequired()
        //    .HasMaxLength(50);

        builder.Property(p => p.MacropadSecretToken)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(p => p.AppUser)
            .WithMany(p => p.Macropads)
            .HasForeignKey(p => p.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasOne(p => p.MacropadModel)
        //    .WithMany(p => p.Macropads)
        //    .HasForeignKey(p => p.MacropadModelId)
        //    .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
