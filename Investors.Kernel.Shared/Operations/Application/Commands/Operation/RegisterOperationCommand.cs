using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Services;
using Investors.Shared.Domain;
using MediatR;


namespace Investors.Kernel.Shared.Operations.Application.Commands.Operation
{
    public record RegisterOperationRequest(
        int Order,
        string Description,
        string Alias,
        string UrlLogo,
        string UserName,
        string Password,
        string Email);

    public sealed record RegisterOperationCommand(Guid UserId, RegisterOperationRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterOperationHandler : IRequestHandler<RegisterOperationCommand, Result<ResponseDefault>>
    {
        private readonly IOperationManager _operationManager;
        public RegisterOperationHandler(IOperationManager userManager)
        {
            _operationManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterOperationCommand request, CancellationToken cancellationToken)
        {
            var user = await _operationManager.Operations.Register(request.Item.Order, request.Item.Description, request.Item.Alias, request.Item.UrlLogo, request.UserId,
                request.Item.UserName, request.Item.Password, request.Item.Email, cancellationToken);
            return user;
        }
    }
}