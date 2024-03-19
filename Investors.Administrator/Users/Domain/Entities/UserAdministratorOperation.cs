using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Users.Domain.Entities
{
    public class UserAdministratorOperation : BaseEntity<int>
    {
        public virtual int UserAdministratorId { get; private set; }
        public int OperationId { get; private set; }
        public virtual UserAdministrator UserAdministrator { get; private set; }
        public virtual Operation Operation { get; private set; }

        private UserAdministratorOperation(
            int operationId,
            Guid createdBy,
            DateTime createdOn,
            Status status) : base(createdBy, createdOn, status)
        {
            OperationId = operationId;
        }

        protected UserAdministratorOperation()
        {

        }

        public static Result<UserAdministratorOperation> Create(
            int operationId,
            Guid createdBy)
        {
            Guard.Against.NegativeOrZero(operationId, nameof(operationId));
            return Result.Success(new UserAdministratorOperation(
                operationId,
                createdBy,
                DateTime.Now,
                Status.Active));
        }

        /// <summary>
        /// Actualizo la operacion
        /// </summary>
        /// <param name="userInSession"></param>
        /// <returns></returns>
        public Result UpdateOperation(Guid userInSession)
        {
            Update(userInSession);
            return Result.Success();
        }

        /// <summary>
        /// Elimino la operacion del usuario
        /// </summary>
        /// <param name="userInSession"></param>
        public Result DeleteOperation(Guid userInSession)
        {
            Delete(userInSession);
            return Result.Success();
        }
    }
}