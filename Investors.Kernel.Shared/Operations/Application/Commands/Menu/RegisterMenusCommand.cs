using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Menu
{

    public sealed record RegisterMenusCommand(Guid UserId, IEnumerable<MenuRequest> Menus, int OperationId, int RestaurantId, int MenuTypeId) : IRequest<Result<ResponseDefault>>;

    public class RegisterMenusHandler : IRequestHandler<RegisterMenusCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public RegisterMenusHandler(IOperationManager userManager)
        {
            _operationManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterMenusCommand request, CancellationToken cancellationToken)
        {
            var user = await _operationManager.Operations.RegisterMenus(request.Menus, request.OperationId, request.RestaurantId, request.MenuTypeId, request.UserId, cancellationToken);
            return user;
        }
    }
}