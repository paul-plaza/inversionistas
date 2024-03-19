using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Repository.EF.Shared;
using MediatR;

namespace Investors.Repository.EF.Events
{
    public class EventManager : IEventDetailManager
    {
        private readonly InvestorsDbContext _repositoryContext;

        private readonly Lazy<EventDetailService> _eventDetailService;

        public EventManager(InvestorsDbContext repositoryContext, ISender sender)
        {
            _repositoryContext = repositoryContext;

            _eventDetailService = new Lazy<EventDetailService>(() => new EventDetailService(new EventRepository(_repositoryContext), sender));
        }
        public EventDetailService Events => _eventDetailService.Value;
    }
}