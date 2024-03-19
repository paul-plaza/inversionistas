using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.Event.Create
{
    public sealed record CreateEventCommand(Guid UserId, CreateEventRequest Item) : IRequest<Result<ResponseDefault>>;

    public class CreateEventHandler : IRequestHandler<CreateEventCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public CreateEventHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .CreateEvent(
                    request.Item.Description,
                    request.Item.Order,
                    request.UserId,
                    cancellationToken
                    );
            return user;
        }
    }
}