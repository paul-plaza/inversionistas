using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.MenuType
{
    public record UpdateMenuTypesRequest(int Id, string Description, string UrlLogo);

    public sealed record UpdateMenuTypesCommand(int OperationId, int RestaurantId, Guid UserId, UpdateMenuTypesRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateMenuTypesHandler : IRequestHandler<UpdateMenuTypesCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public UpdateMenuTypesHandler(IOperationManager userManager)
        {
            _operationManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateMenuTypesCommand request, CancellationToken cancellationToken)
        {
            var user = await _operationManager
                .Operations
                .UpdateMenuTypes(
                    request.Item.Id,
                    request.OperationId,
                    request.RestaurantId,
                    request.Item.Description,
                    request.Item.UrlLogo,
                    request.UserId, cancellationToken);
            return user;
        }
    }
}