using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MacropadServer.Infrastructure.Context;

namespace MacropadServer.Infrastructure.Repositories;
internal sealed class MacropadInputRepository : Repository<MacropadInput, ApplicationDbContext>, IMacropadInputRepository
{
    public MacropadInputRepository(ApplicationDbContext context) : base(context)
    {
    }
}
