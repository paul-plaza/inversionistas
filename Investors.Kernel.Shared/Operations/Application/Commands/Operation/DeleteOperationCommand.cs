using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Commands.Operation
{
    public sealed record DeleteOperationCommand(Guid userId, int idOperation) : IRequest<Result<ResponseDefault>>;

    public class DeleteOperationHandler : IRequestHandler<DeleteOperationCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public DeleteOperationHandler(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }
        public async Task<Result<ResponseDefault>> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationManager.Operations.DeleteOperation(request.idOperation, request.userId, cancellationToken);
            return operation;
        }
    }
}
