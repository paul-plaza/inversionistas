using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.Entities;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Users.Domain.Aggregate
{
    public class UserAdministrator : BaseEntity<int>, IAggregateRoot
    {
        public const int EmailMaxLength = 300;
        public Email Email { get; private set; }

        private readonly List<UserAdministratorOption> _options = new();

        public virtual IReadOnlyList<UserAdministratorOption> Options => _options;

        private readonly List<UserAdministratorOperation> _operations = new();

        public virtual IReadOnlyList<UserAdministratorOperation> Operations => _operations;

        private UserAdministrator(
            Email email,
            List<UserAdministratorOption> options,
            List<UserAdministratorOperation> operations,
            Guid createdBy,
            DateTime createdOn,
            Status status) : base(createdBy, createdOn, status)
        {
            Email = email;
            _options = options;
            _operations = operations;
        }

        protected UserAdministrator()
        {

        }

        public static Result<UserAdministrator> Create(
            Email email,
            List<int> administratorOptions,
            List<int> administratorOperations,
            Guid createdBy)
        {
            Guard.Against.NullOrEmpty(email, nameof(email));

            Guard.Against.NullOrEmpty(administratorOptions, nameof(administratorOptions));
            Guard.Against.NullOrEmpty(administratorOperations, nameof(administratorOptions));

            return Result.Success(new UserAdministrator(
                email,
                administratorOptions.Select(x => UserAdministratorOption.Create(x, createdBy).Value).ToList(),
                administratorOperations.Select(x => UserAdministratorOperation.Create(x, createdBy).Value).ToList(),
                createdBy,
                DateTime.Now,
                Status.Active));
        }

        /// <summary>
        /// actualizo datos y permisos del usuario
        /// </summary>
        /// <param name="email"></param>
        /// <param name="options"></param>
        /// <param name="operations"></param>
        /// <param name="userInSession"></param>
        /// <returns></returns>
        public Result UpdateUser(
            Email email,
            List<int> options,
            List<int> operations,
            Guid userInSession)
        {
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(options, nameof(options));
            Guard.Against.NullOrEmpty(operations, nameof(options));

            //gestiono opciones

            var optionsToDelete = from o in _options
                where options.Contains(o.OptionId) is false
                select o.DeleteOption(userInSession);

            if (optionsToDelete.Any(x => x.IsFailure))
            {
                return Result.Failure("No se pudo eliminar una o mas opciones");
            }

            var optionsToUpdate = from o in _options
                where options.Contains(o.OptionId)
                select o.UpdateOption(userInSession);

            if (optionsToUpdate.Any(x => x.IsFailure))
            {
                return Result.Failure("No se pudo actualizar una o mas opciones");
            }

            var optionsToAdd = options
                .Where(x => _options.Select(o => o.OptionId).Contains(x) is false)
                .Select(x => UserAdministratorOption.Create(x, userInSession).Value)
                .ToList();

            _options.AddRange(optionsToAdd);

            //gestiono operaciones

            var operationsToDelete = from o in _operations
                where operations.Contains(o.OperationId) is false
                select o.DeleteOperation(userInSession);

            if (operationsToDelete.Any(x => x.IsFailure))
            {
                return Result.Failure("No se pudo eliminar una o mas operaciones");
            }

            var operationsToUpdate = from o in _operations
                where operations.Contains(o.OperationId)
                select o.UpdateOperation(userInSession);

            if (operationsToUpdate.Any(x => x.IsFailure))
            {
                return Result.Failure("No se pudo actualizar una o mas operaciones");
            }

            var operationsToAdd = operations
                .Where(x => _operations.Select(o => o.OperationId).Contains(x) is false)
                .Select(x => UserAdministratorOperation.Create(x, userInSession).Value)
                .ToList();

            _operations.AddRange(operationsToAdd);

            Email = email;
            Update(userInSession);

            return Result.Success();
        }

        /// <summary>
        ///     Elimino usuario
        /// </summary>
        /// <param name="userInSession"></param>
        /// <returns></returns>
        public void DeleteUserProfile(Guid userInSession)
        {
            _options.ForEach(x => x.DeleteOption(userInSession));

            _operations.ForEach(x => x.DeleteOperation(userInSession));

            Delete(userInSession);
        }

    }
}