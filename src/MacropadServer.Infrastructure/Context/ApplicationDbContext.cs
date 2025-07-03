using ED.GenericRepository;
using MacropadServer.Domain.Abstractions;
using MacropadServer.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Infrastructure.Context;
internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
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
        //HttpContextAccessor httpContextAccessor = new();
        string? userName = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(p => p.Type == "UserName")?.Value;
        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt)
                    .CurrentValue = DateTimeOffset.UtcNow;
                entry.Property(p => p.CreatedBy)
                    .CurrentValue = userName!;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeleteAt)
                        .CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property(p => p.DeleteBy)
                    .CurrentValue = userName;
                }
                else
                {
                    entry.Property(p => p.UpdatedAt)
                        .CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property(p => p.UpdatedBy)
                    .CurrentValue = userName;
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Db'den silme işlemi yapamazsınız!");
            }

            //switch (entry.State)
            //{
            //    case EntityState.Added:
            //        entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            //        break;
            //    case EntityState.Modified:
            //        entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            //        break;
            //}

        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
