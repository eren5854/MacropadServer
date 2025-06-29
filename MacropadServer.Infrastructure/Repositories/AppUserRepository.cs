using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MacropadServer.Infrastructure.Context;

namespace MacropadServer.Infrastructure.Repositories;
internal sealed class AppUserRepository : Repository<AppUser, ApplicationDbContext>, IAppUserRepository
{
    public AppUserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
