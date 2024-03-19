using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Room
{
    public sealed record DeleteRoomCommand(Guid userId, int restaurantId, int operationId) : IRequest<Result<ResponseDefault>>;

    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public DeleteRoomHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _operationManager.Operations.DeleteRoom(request.restaurantId, request.userId, request.operationId, cancellationToken);
            return restaurant;
        }
    }
}
