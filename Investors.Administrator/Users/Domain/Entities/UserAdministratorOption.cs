using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Users.Domain.Entities
{
    public class UserAdministratorOption : BaseEntity<int>
    {
        public virtual int UserAdministratorId { get; private set; }
        public int OptionId { get; private set; }
        public virtual UserAdministrator UserAdministrator { get; private set; }
        public virtual Option Option { get; private set; }

        private UserAdministratorOption(
            int optionId,
            Guid createdBy,
            DateTime createdOn,
            Status status) : base(createdBy, createdOn, status)
        {
            OptionId = optionId;
        }

        protected UserAdministratorOption()
        {

        }

        public static Result<UserAdministratorOption> Create(
            int optionId,
            Guid createdBy)
        {
            Guard.Against.NegativeOrZero(optionId, nameof(optionId));
            return Result.Success(new UserAdministratorOption(
                optionId,
                createdBy,
                DateTime.Now,
                Status.Active));
        }

        /// <summary>
        /// Actualizo la opcion
        /// </summary>
        /// <param name="userInSession"></param>
        /// <returns></returns>
        public Result UpdateOption(Guid userInSession)
        {
            Update(userInSession);
            return Result.Success();
        }

        /// <summary>
        /// Elimino la opcion del usuario
        /// </summary>
        /// <param name="userInSession"></param>
        public Result DeleteOption(Guid userInSession)
        {
            Delete(userInSession);
            return Result.Success();
        }
    }
}