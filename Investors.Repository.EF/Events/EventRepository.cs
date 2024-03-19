using Ardalis.Specification.EntityFrameworkCore;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Events
{
    public class EventRepository : RepositoryBase<Event>, IEventWriteRepository, IEventReadRepository
    {
        public EventRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}