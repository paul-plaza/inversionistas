using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.EventSubDetail
{
    public record UpdateEventSubDetailRequest(
        int EventId,
        int EventSubDetailId,
        int EventDetailId,
        string Description,
        string Title,
        string Image);

    public sealed record UpdateEventSubDetailCommand(Guid UserId, UpdateEventSubDetailRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateEventSubDetailHandler : IRequestHandler<UpdateEventSubDetailCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public UpdateEventSubDetailHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateEventSubDetailCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .UpdateEventSubDetail(
                    request.Item.EventId,
                    request.Item.EventSubDetailId,
                    request.Item.EventDetailId,
                    request.Item.Description,
                    request.Item.Title,
                    request.Item.Image,
                    request.UserId,
                    cancellationToken);
            return user;
        }
    }
}