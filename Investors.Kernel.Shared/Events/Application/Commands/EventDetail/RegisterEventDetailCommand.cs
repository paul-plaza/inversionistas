using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.EventDetail
{
    public record RegisterEventDetailRequest(int OperationId, string Title);

    public sealed record RegisterEventDetailCommand(Guid UserId, int EventId, RegisterEventDetailRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterEventDetailHandler : IRequestHandler<RegisterEventDetailCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _eventDetailManager;
        public RegisterEventDetailHandler(IEventDetailManager userManager)
        {
            _eventDetailManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterEventDetailCommand request, CancellationToken cancellationToken)
        {
            var user = await _eventDetailManager
                .Events
                .AddEventDetail(
                    request.Item.OperationId,
                    request.EventId,
                    null,
                    null,
                    request.Item.Title,
                    null,
                    request.UserId,
                    cancellationToken);
            return user;
        }
    }
}