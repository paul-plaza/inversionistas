using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.EventSubDetail
{
    public sealed record DeleteEventSubDetailCommand(
        Guid UserId,
        int EventId,
        int EventDetailId,
        int EventSubDetailId) : IRequest<Result<ResponseDefault>>;

    public class DeleteOperationHandler : IRequestHandler<DeleteEventSubDetailCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public DeleteOperationHandler(IEventDetailManager operationManager)
        {
            _eventDetailManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteEventSubDetailCommand request, CancellationToken cancellationToken)
        {
            var subDetail = await _eventDetailManager
                .Events
                .DeleteEventSubDetail(
                    eventId: request.EventId,
                    eventDetailId: request.EventDetailId,
                    eventSubDetailId: request.EventSubDetailId,
                    userId: request.UserId,
                    cancellationToken);
            return subDetail;
        }
    }
}