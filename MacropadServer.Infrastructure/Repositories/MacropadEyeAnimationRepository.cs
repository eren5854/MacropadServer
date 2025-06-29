using ED.GenericRepository;
using MacropadServer.Domain.Entities;
using MacropadServer.Domain.Repositories;
using MacropadServer.Infrastructure.Context;

namespace MacropadServer.Infrastructure.Repositories;
internal sealed class MacropadEyeAnimationRepository : Repository<MacropadEyeAnimation, ApplicationDbContext>, IMacropadEyeAnimationRepository
{
    public MacropadEyeAnimationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
