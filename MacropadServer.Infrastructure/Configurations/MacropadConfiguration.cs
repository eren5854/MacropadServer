using MacropadServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MacropadServer.Infrastructure.Configurations;
public sealed class MacropadConfiguration : IEntityTypeConfiguration<Macropad>
{
    public void Configure(EntityTypeBuilder<Macropad> builder)
    {
        builder.ToTable("Macropads");

        builder.Property(p => p.MacropadName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.MacropadSerialNo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.MacropadSecretToken)
            .IsRequired()
            .HasMaxLength(100);



        builder.HasQueryFilter(filter => !filter.IsDeleted);
    }
}
