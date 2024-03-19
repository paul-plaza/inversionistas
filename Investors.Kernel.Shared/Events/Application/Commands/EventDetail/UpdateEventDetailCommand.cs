using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.EventDetail
{
    public record UpdateEventDetailRequest(
        int Id,
        int OperationId,
        string Title,
        string? UrlLogo,
        string? Description,
        string? UrlToOpen);

    public sealed record UpdateEventDetailCommand(Guid UserId, int EventId, UpdateEventDetailRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateEventDetailHandler : IRequestHandler<UpdateEventDetailCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public UpdateEventDetailHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateEventDetailCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .UpdateEventDetail(
                    eventDetailId: request.Item.Id,
                    eventId: request.EventId,
                    operationId: request.Item.OperationId,
                    title: request.Item.Title,
                    request.Item.UrlLogo,
                    request.UserId,
                    cancellationToken);
            return user;
        }
    }
}