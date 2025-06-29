using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MacropadServer.Infrastructure.Context;

namespace MacropadServer.Infrastructure.Repositories;
internal sealed class MacropadRepository : Repository<Macropad, ApplicationDbContext>, IMacropadRepository
{
    public MacropadRepository(ApplicationDbContext context) : base(context)
    {
    }
}
