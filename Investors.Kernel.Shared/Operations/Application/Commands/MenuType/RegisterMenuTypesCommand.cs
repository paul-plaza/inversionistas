using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.MenuType
{
    public record RegisterMenuTypesRequest(string Description, string UrlLogo);

    public sealed record RegisterMenuTypesCommand(Guid UserId, int OperationId, int RestaurantId, RegisterMenuTypesRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterMenuTypesHandler : IRequestHandler<RegisterMenuTypesCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public RegisterMenuTypesHandler(IOperationManager userManager)
        {
            _operationManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterMenuTypesCommand request, CancellationToken cancellationToken)
        {
            var user = await _operationManager.Operations.RegisterMenuTypes(request.OperationId, request.RestaurantId, request.Item.Description, request.Item.UrlLogo,
                request.UserId, cancellationToken);
            return user;
        }
    }
}