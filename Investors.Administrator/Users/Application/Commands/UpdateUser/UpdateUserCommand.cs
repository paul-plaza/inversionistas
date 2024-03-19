using CSharpFunctionalExtensions;
using Investors.Administrator.Options.Application.Querys.GetOptions;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Administrator.Users.Specifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Shared.Domain.ValueObjects;
using MediatR;

namespace Investors.Administrator.Users.Application.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(UpdateUserRequest User, Guid UserInSession) : IRequest<Result<UpdateUserResponse>>;

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse>>
    {
        private readonly IUserAdministratorWriteRepository _repositoryUserAdmin;
        private readonly ISender _sender;

        public UpdateUserHandler(IUserAdministratorWriteRepository repositoryUserAdmin, ISender sender)
        {
            _repositoryUserAdmin = repositoryUserAdmin;
            _sender = sender;
        }
        public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _repositoryUserAdmin.SingleOrDefaultAsync(new UserAdminByIdSpecs(request.User.Id), cancellationToken);

            if (user is null)
            {
                return Result.Failure<UpdateUserResponse>("Usuario no encontrado");
            }

            var ensureCreateEmail = Email.Create(request.User.Email);

            if (ensureCreateEmail.IsFailure)
            {
                return Result.Failure<UpdateUserResponse>(ensureCreateEmail.Error);
            }

            var options = await _sender.Send(new GetOptionsQuery(), cancellationToken);

            if (options.IsFailure)
            {
                return Result.Failure<UpdateUserResponse>(options.Error);
            }

            var optionsToSave = request.User.Options
                .All(o => options.Value.Select(x => x.Id).Contains(o));

            if (optionsToSave is false)
            {
                return Result.Failure<UpdateUserResponse>("Algunas de las opciones no existen");
            }

            var operations = await _sender.Send(new GetOperationsQuery(), cancellationToken);

            if (operations.IsFailure)
            {
                return Result.Failure<UpdateUserResponse>(operations.Error);
            }

            var operationsToSave = request.User.Operations
                .All(o => operations.Value.Select(x => x.Id).Contains(o));

            if (operationsToSave is false)
            {
                return Result.Failure<UpdateUserResponse>("Algunas de las operaciones no existen");
            }

            user.UpdateUser(ensureCreateEmail.Value, request.User.Options, request.User.Operations, request.UserInSession);

            await _repositoryUserAdmin.SaveChangesAsync(cancellationToken);
            return Result.Success(new UpdateUserResponse());
        }
    }
}