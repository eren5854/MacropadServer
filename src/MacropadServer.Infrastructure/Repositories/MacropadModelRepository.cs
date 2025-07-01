using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MacropadServer.Infrastructure.Context;

namespace MacropadServer.Infrastructure.Repositories;
internal sealed class MacropadModelRepository : Repository<MacropadModel, ApplicationDbContext>, IMacropadModelRepository
{
    public MacropadModelRepository(ApplicationDbContext context) : base(context)
    {
    }
}
