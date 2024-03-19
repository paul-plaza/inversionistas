namespace Investors.Client.Users.Domain.Entities.Transactions
{
    public class CashbackDetail : BaseEntity<int>
    {
        public int MenuId { get; private set; }

        public int Points { get; private set; }

        //relacion con movimientos
        public Movement Movement { get; private set; }
        public Guid MovementId { get; private set; }

        protected CashbackDetail()
        {

        }

        private CashbackDetail(Guid movementId,
            int menuId,
            int points,
            Guid userInSession,
            DateTime createdOn,
            Status status) : base(userInSession, createdOn, status)
        {
            MovementId = movementId;
            MenuId = menuId;
            Points = points;
        }

        public static Result<CashbackDetail> Create(Guid movementId, int menuId, int points, Guid userInSession)
        {
            Guard.Against.NegativeOrZero(menuId);
            Guard.Against.NegativeOrZero(points);

            return Result.Success(new CashbackDetail(movementId, menuId, points, userInSession, DateTime.Now, Status.Active));
        }
    }
}