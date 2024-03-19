using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Domain.Aggregate;

namespace Investors.Kernel.Shared.Events.Repositories
{
    public interface IEventReadRepository : IReadRepositoryBase<Event>
    {
    }
    public interface IEventWriteRepository : IRepositoryBase<Event>
    {
    }

}