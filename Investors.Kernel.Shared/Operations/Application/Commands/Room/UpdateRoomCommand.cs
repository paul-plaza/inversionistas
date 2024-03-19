using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Room
{
    public record UpdateRoomRequest(
        int Id,
        string Description,
        string Title,
        string UrlLogo,
        string Observation,
        int Guests,
        int TypeRoom);

    public sealed record UpdateRoomCommand(int OperationId, Guid UserId, UpdateRoomRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public UpdateRoomHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationManager.Operations.UpdateRoom(request.Item.Id, request.OperationId, request.Item.Description,
                request.Item.Title, request.Item.UrlLogo, request.Item.Observation, request.Item.Guests, request.Item.TypeRoom, request.UserId, cancellationToken);
            return operation;
        }
    }
}