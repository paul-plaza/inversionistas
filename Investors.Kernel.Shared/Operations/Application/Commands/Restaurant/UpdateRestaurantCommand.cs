using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Restaurant
{

    public record UpdateRestaurantRequest(int Id, string Description, string UrlLogo, string Email);

    public sealed record UpdateRestaurantCommand(int OperationId, Guid UserId, UpdateRestaurantRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public UpdateRestaurantHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationManager.Operations.UpdateRestaurant(
                request.Item.Id,
                request.OperationId,
                request.Item.Description,
                request.Item.UrlLogo,
                request.Item.Email,
                request.UserId,
                cancellationToken);
            return operation;
        }
    }
}