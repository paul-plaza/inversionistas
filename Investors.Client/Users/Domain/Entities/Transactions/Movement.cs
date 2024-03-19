using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;

namespace Investors.Client.Users.Domain.Entities.Transactions
{
    public class Movement : BaseEntity<Guid>
    {
        public TransactionType TransactionType { get; private set; }
        public TransactionState TransactionState { get; private set; }
        public int OperationId { get; private set; }
        public int? RestaurantId { get; private set; }

        public int? RoomId { get; private set; }
        public int? TotalItems { get; private set; }
        public int TotalToRedeem { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<CashbackDetail> _cashbackDetail = new();

        /// <summary>
        /// Detalle de cashback 
        /// </summary>
        public virtual IReadOnlyList<CashbackDetail> CashbackDetails => _cashbackDetail;

        /// <summary>
        /// Noche de hospedaje
        /// </summary>
        public virtual NightsDetail NightsDetail { get; private set; }

        public virtual User User { get; private set; }
        public virtual Guid UserId { get; private set; }

        /// <summary>
        /// Habitacion de movimiento de noche
        /// </summary>
        public virtual Room Room { get; private set; }

        protected Movement()
        {

        }

        private Movement(
            Guid id,
            Guid userId,
            TransactionType transaction,
            TransactionState transactionState,
            int operationId,
            int totalToRedeem,
            Guid userInSession,
            DateTime createdOn,
            Status status,
            List<CashbackDetail>? cashbackDetail = null,
            int? restaurantId = null,
            NightsDetail? nightsDetail = null,
            int? roomId = null
            ) : base(userInSession, createdOn, status)
        {
            Id = id;
            UserId = userId;
            TransactionType = transaction;
            TransactionState = transactionState;
            OperationId = operationId;
            TotalToRedeem = totalToRedeem;

            if (transaction == TransactionType.Nights)
            {
                NightsDetail = nightsDetail!;
                RoomId = roomId;
            }

            if (transaction == TransactionType.Cashback)
            {
                _cashbackDetail.AddRange(cashbackDetail!);
                RestaurantId = restaurantId;
                TotalItems = cashbackDetail.Count;
            }

        }

        public static Result<Movement> CreateCashback(
            Guid userId,
            int operationId,
            IEnumerable<MenuResponse> menus,
            int restaurantId,
            Guid userInSession)
        {
            Guard.Against.NullOrEmpty(userId);
            Guard.Against.NullOrEmpty(userInSession);

            List<CashbackDetail> cashbackDetails = new List<CashbackDetail>();
            Guid movementId = Guid.NewGuid();

            foreach (var menu in menus)
            {
                Result<CashbackDetail> ensureCashbackDetailCreated = CashbackDetail.Create(
                    movementId,
                    menu.Id,
                    menu.Points,
                    userInSession);
                if (ensureCashbackDetailCreated.IsFailure)
                {
                    return Result.Failure<Movement>(ensureCashbackDetailCreated.Error);
                }
                cashbackDetails.Add(ensureCashbackDetailCreated.Value);
            }

            return Result.Success(
                new Movement(movementId,
                    userId,
                    TransactionType.Cashback,
                    TransactionState.Requested,
                    operationId,
                    cashbackDetails.Sum(x => x.Points),
                    userInSession,
                    DateTime.Now,
                    Status.Active,
                    cashbackDetail: cashbackDetails,
                    restaurantId: restaurantId
                    ));
        }

        public static Result<Movement> CreateNights(
            Guid userId,
            int operationId,
            string? observation,
            int totalNights,
            int roomId,
            DateTime dateStart,
            DateTime dateEnd,
            Guid userInSession)
        {
            Guard.Against.NullOrEmpty(userId);
            Guard.Against.NullOrEmpty(userInSession);

            Guid movementId = Guid.NewGuid();

            Result<NightsDetail> ensureNightsDetailCreated = NightsDetail.Create(
                movementId,
                dateStart,
                dateEnd,
                observation, userInSession);

            if (ensureNightsDetailCreated.IsFailure)
            {
                return Result.Failure<Movement>(ensureNightsDetailCreated.Error);
            }

            return Result.Success(
                new Movement(movementId,
                    userId,
                    TransactionType.Nights,
                    TransactionState.Requested,
                    operationId,
                    totalNights,
                    userInSession,
                    DateTime.Now,
                    Status.Active,
                    nightsDetail: ensureNightsDetailCreated.Value,
                    roomId: roomId));
        }

        public Result UpdateMovement(TransactionState transactionState, Guid userInSession)
        {
            Update(userInSession);
            TransactionState = transactionState;
            return Result.Success();
        }
    }
}