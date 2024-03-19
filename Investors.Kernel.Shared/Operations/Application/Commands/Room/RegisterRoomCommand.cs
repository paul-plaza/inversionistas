using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Room
{
    public record RegisterRoomRequest(
        string Description,
        string Title,
        string UrlLogo,
        int Guests,
        int TypeRoom,
        string Observation);

    public sealed record RegisterRoomCommand(Guid UserId, int OperationId, RegisterRoomRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterRoomHandler : IRequestHandler<RegisterRoomCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public RegisterRoomHandler(IOperationManager userManager)
        {
            _operationManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterRoomCommand request, CancellationToken cancellationToken)
        {
            var user = await _operationManager
                .Operations
                .RegisterRoom(
                    request.OperationId,
                    request.Item.Description,
                    request.Item.Title,
                    request.Item.UrlLogo,
                    request.Item.Guests,
                    request.Item.TypeRoom,
                    request.Item.Observation,
                    request.UserId, cancellationToken);
            return user;
        }
    }
}