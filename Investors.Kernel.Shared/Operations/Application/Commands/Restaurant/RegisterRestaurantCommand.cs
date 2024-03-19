using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Restaurant
{
    public record RegisterRestaurantRequest(string Description, string UrlLogo, string Email);

    public sealed record RegisterRestaurantCommand(int OperationId, Guid UserId, RegisterRestaurantRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterRestaurantHandler : IRequestHandler<RegisterRestaurantCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public RegisterRestaurantHandler(IOperationManager userManager)
        {
            _operationManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterRestaurantCommand request, CancellationToken cancellationToken)
        {
            var user = await _operationManager
                .Operations.RegisterRestaurant(
                    request.OperationId,
                    request.Item.Description,
                    request.Item.UrlLogo,
                    request.Item.Email,
                    request.UserId,
                    cancellationToken);
            return user;
        }
    }
}