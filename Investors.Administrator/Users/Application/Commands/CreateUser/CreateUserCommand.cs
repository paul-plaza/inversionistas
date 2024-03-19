using CSharpFunctionalExtensions;
using Investors.Administrator.Options.Application.Querys.GetOptions;
using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Shared.Domain.ValueObjects;
using MediatR;

namespace Investors.Administrator.Users.Application.Commands.CreateUser
{
    public sealed record CreateUserCommand(UserToCreateRequest User, Guid UserInSession) : IRequest<Result<UserCreatedResponse>>;

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserCreatedResponse>>
    {
        private readonly IUserAdministratorWriteRepository _repositoryUserAdmin;
        private readonly ISender _sender;
        private readonly IRepositoryOptionForRead _repositoryOptionFor;

        public CreateUserHandler(IUserAdministratorWriteRepository repositoryUserAdmin, ISender sender)
        {
            _repositoryUserAdmin = repositoryUserAdmin;
            _sender = sender;
        }
        public async Task<Result<UserCreatedResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var ensureCreateEmail = Email.Create(request.User.Email);

            if (ensureCreateEmail.IsFailure)
            {
                return Result.Failure<UserCreatedResponse>(ensureCreateEmail.Error);
            }

            var options = await _sender.Send(new GetOptionsQuery(), cancellationToken);

            if (options.IsFailure)
            {
                return Result.Failure<UserCreatedResponse>(options.Error);
            }

            var optionsToSave = request.User.Options
                .All(o => options.Value.Select(x => x.Id).Contains(o));

            if (optionsToSave is false)
            {
                return Result.Failure<UserCreatedResponse>("Algunas de las opciones no existen");
            }

            var operations = await _sender.Send(new GetOperationsQuery(), cancellationToken);

            if (operations.IsFailure)
            {
                return Result.Failure<UserCreatedResponse>(operations.Error);
            }

            var operationsToSave = request.User.Operations
                .All(o => operations.Value.Select(x => x.Id).Contains(o));

            if (operationsToSave is false)
            {
                return Result.Failure<UserCreatedResponse>("Algunas de las operaciones no existen");
            }

            var ensureUserToCreate = UserAdministrator.Create(
                ensureCreateEmail.Value,
                request.User.Options,
                request.User.Operations,
                request.UserInSession);

            if (ensureUserToCreate.IsFailure)
            {
                return Result.Failure<UserCreatedResponse>(ensureUserToCreate.Error);
            }

            await _repositoryUserAdmin.AddAsync(ensureUserToCreate.Value, cancellationToken);

            await _repositoryUserAdmin.SaveChangesAsync(cancellationToken);
            return Result.Success(new UserCreatedResponse());
        }
    }
}