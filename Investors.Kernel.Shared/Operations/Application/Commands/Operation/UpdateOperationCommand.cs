using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Operation
{

    public record UpdateOperationRequest(
        int Id,
        int Order,
        string Description,
        string Alias,
        string UrlLogo,
        string UserName,
        string Password,
        string Email);
    public sealed record UpdateOperationCommand(Guid userId, UpdateOperationRequest item) : IRequest<Result<ResponseDefault>>;

    public class UpdateOperationHandler : IRequestHandler<UpdateOperationCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public UpdateOperationHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationManager.Operations.Update(request.item.Id, request.item.Order, request.item.Description, request.item.Alias, request.item.UrlLogo,
                request.item.UserName, request.item.Password, request.item.Email, request.userId, cancellationToken);
            return operation;
        }
    }
}