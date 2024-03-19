using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.EventSubDetail
{
    public record RegisterEventSubDetailRequest(
        string? Description,
        string? Title,
        string? Image);

    public sealed record RegisterEventSubDetailCommand(Guid UserId,
        int EventId,
        int EventDetailId,
        RegisterEventSubDetailRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterEventSubDetailHandler : IRequestHandler<RegisterEventSubDetailCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public RegisterEventSubDetailHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterEventSubDetailCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .AddEventSubDetail(
                    request.EventId,
                    request.EventDetailId,
                    request.Item.Description,
                    request.Item.Title,
                    request.Item.Image,
                    request.UserId, cancellationToken);
            return user;
        }
    }
}