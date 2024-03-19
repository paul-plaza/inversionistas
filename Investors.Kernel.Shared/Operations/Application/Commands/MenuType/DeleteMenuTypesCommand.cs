using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.MenuType
{
    public sealed record DeleteMenuTypesCommand(Guid UserId, int RestaurantId, int OperationId, int MenuTypesId) : IRequest<Result<ResponseDefault>>;

    public class DeleteMenuTypesHandler : IRequestHandler<DeleteMenuTypesCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public DeleteMenuTypesHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteMenuTypesCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _operationManager
                .Operations
                .DeleteMenuTypes(
                    request.RestaurantId,
                    request.UserId,
                    request.OperationId,
                    request.MenuTypesId,
                    cancellationToken);
            return restaurant;
        }
    }
}