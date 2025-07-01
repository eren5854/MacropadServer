using ED.GenericRepository;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Infrastructure.Context;
internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();

        builder.Entity<IdentityUserLogin<Guid>>()
        .HasKey(l => new { l.LoginProvider, l.ProviderKey });


        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;
            }

            //if(entry.State == EntityState.Modified)
            //{
            //    if (entry.Property(p => p.IsDeleted).CurrentValue = true)
            //    {
            //        entry.Property(p => p.DeleteAt)
            //            .CurrentValue = DateTimeOffset.UtcNow;
            //    }
            //    else
            //    {
            //        entry.Property(p => p.UpdatedAt)
            //            .CurrentValue = DateTimeOffset.UtcNow;
            //    }
            //}

            //if(entry.State == EntityState.Deleted)
            //{
            //    throw new ArgumentException("Db'den silme işlemi yapamazsınız!");
            //}
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
