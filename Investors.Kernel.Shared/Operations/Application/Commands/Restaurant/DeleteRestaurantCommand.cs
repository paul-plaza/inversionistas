using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Restaurant
{
    public sealed record DeleteRestaurantCommand(Guid UserId, int RestaurantId, int OperationId) : IRequest<Result<ResponseDefault>>;

    public class DeleteRestaurantHandler : IRequestHandler<DeleteRestaurantCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public DeleteRestaurantHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _operationManager.Operations.DeleteRestaurant(request.RestaurantId, request.UserId, request.OperationId, cancellationToken);
            return restaurant;
        }
    }
}