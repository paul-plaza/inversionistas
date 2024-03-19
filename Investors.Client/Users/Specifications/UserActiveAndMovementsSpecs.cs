using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{

    internal sealed class UserActiveAndMovementsSpecs : Specification<User>, ISingleResultSpecification<User>
    {
        /// <summary>
        /// Constructor specificacion consultar usuario por id
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idMovement"></param>
        /// <param name="transactionType"></param>
        public UserActiveAndMovementsSpecs(Guid idUser, Guid idMovement, TransactionType transactionType)
        {
            Query.Include(x => x.Profile);

            if (transactionType == TransactionType.Cashback)
            {
                Query.Include(x =>
                        x.Movements.Where(y => y.Status == Status.Active
                                               && y.Id == idMovement
                                               && y.TransactionState == TransactionState.Requested))
                    .ThenInclude(x => x.CashbackDetails);
            }

            if (transactionType == TransactionType.Nights)
            {
                Query.Include(x =>
                        x.Movements.Where(y => y.Status == Status.Active
                                               && y.Id == idMovement
                                               && y.TransactionState == TransactionState.Requested))
                    .ThenInclude(x => x.NightsDetail);
            }

            Query.Where(x => x.Id == idUser && x.Status == Status.Active);

            Query.AsSplitQuery();
        }
    }
}