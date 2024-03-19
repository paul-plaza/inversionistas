using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.Event.Delete
{

    public sealed record DeleteEventCommand(Guid UserId, int EventId) : IRequest<Result<ResponseDefault>>;

    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public DeleteEventHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .DeleteEvent(
                    eventId: request.EventId,
                    userInSession: request.UserId,
                    cancellationToken: cancellationToken
                    );
            return user;
        }
    }
}