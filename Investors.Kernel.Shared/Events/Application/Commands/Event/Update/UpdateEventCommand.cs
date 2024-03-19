using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.Event.Update
{


    public sealed record UpdateEventCommand(Guid UserId, UpdateEventRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public UpdateEventHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .UpdateEvent(
                    eventId: request.Item.Id,
                    description: request.Item.Description,
                    order: request.Item.Order,
                    userInSession: request.UserId,
                    cancellationToken: cancellationToken
                    );
            return user;
        }
    }
}