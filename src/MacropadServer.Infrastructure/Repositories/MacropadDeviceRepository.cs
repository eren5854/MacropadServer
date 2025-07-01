using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MacropadServer.Infrastructure.Context;

namespace MacropadServer.Infrastructure.Repositories;
internal sealed class MacropadDeviceRepository : Repository<MacropadDevice, ApplicationDbContext>, IMacropadDeviceRepository
{
    public MacropadDeviceRepository(ApplicationDbContext context) : base(context)
    {
    }
}
