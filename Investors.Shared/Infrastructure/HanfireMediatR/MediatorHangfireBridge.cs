using System.ComponentModel;
using MediatR;

namespace Investors.Shared.Infrastructure.HanfireMediatR
{
    public class MediatorHangfireBridge
    {
        private readonly IMediator _mediator;
        public MediatorHangfireBridge(IMediator mediator)
        {
            _mediator = mediator;
        }

        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest request)
        {
            await _mediator.Send(request);
        }

        [DisplayName("{0}")]
        public async Task Send(string jobName, INotification request)
        {
            await _mediator.Publish(request);
        }
    }
}

