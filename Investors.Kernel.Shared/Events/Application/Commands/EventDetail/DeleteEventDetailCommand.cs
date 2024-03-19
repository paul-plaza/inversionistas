using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Services;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Commands.EventDetail
{
    public sealed record DeleteEventDetailCommand(Guid UserId, int EventId, int EventDetailId) : IRequest<Result<ResponseDefault>>;

    public class DeleteOperationHandler : IRequestHandler<DeleteEventDetailCommand, Result<ResponseDefault>>
    {
        private readonly IEventDetailManager _operationManager;
        public DeleteOperationHandler(IEventDetailManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteEventDetailCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationManager.Events.DeleteEventDetail(request.EventDetailId, request.EventId, request.UserId, cancellationToken);
            return operation;
        }
    }
}