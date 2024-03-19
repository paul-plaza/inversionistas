using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Investors.Domain.Entities
{
    public class InvestorOperation : BaseEntity<Guid>
    {
        /// <summary>
        /// Nombre completo del inversionista
        /// </summary>
        public int OperationId { get; private set; }

        public Guid InvestorId { get; private set; }

        public int TotalActions { get; private set; }

        public virtual Operation Operation { get; private set; }

        public virtual Investor Investor { get; private set; }


        protected InvestorOperation()
        {

        }

        private InvestorOperation(int operationId,
            Guid investorId,
            int totalActions,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            OperationId = operationId;
            InvestorId = investorId;
            TotalActions = totalActions;
        }


        public static Result<InvestorOperation> Create(
            int operationId,
            int totalActions,
            Guid investorId,
            Guid createBy)
        {

            if (Errors.Any())
                return Result.Failure<InvestorOperation>(GetErrors());

            return Result.Success(new InvestorOperation(
                operationId,
                investorId,
                totalActions,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public void UpdateOperation(int totalActions, Guid userInSession)
        {
            TotalActions = totalActions;
            Update(userInSession);
        }
        public void DeleteOperation(Guid userInSession)
        {
            Delete(userInSession);
        }
    }
}