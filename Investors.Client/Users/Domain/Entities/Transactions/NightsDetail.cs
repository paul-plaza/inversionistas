namespace Investors.Client.Users.Domain.Entities.Transactions
{
    public class NightsDetail : BaseEntity<int>
    {
        public DateOnly DateStart { get; private set; }
        public DateOnly DateEnd { get; private set; }

        public string? Observation { get; private set; }

        public Movement Movement { get; private set; }
        public Guid MovementId { get; private set; }

        protected NightsDetail()
        {

        }

        private NightsDetail(Guid movementId,
            DateOnly dateStart,
            DateOnly dateEnd,
            string? observation,
            Guid userInSession,
            DateTime createdOn,
            Status status) : base(userInSession, createdOn, status)
        {
            MovementId = movementId;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Observation = observation;
        }

        public static Result<NightsDetail> Create(Guid movementId, DateTime dateStart, DateTime dateEnd, string? observation, Guid userInSession)
        {
            //valido que se reserve al menos 1 día antes
            if (dateStart.Date <= DateTime.Now.AddDays(1).Date)
                return Result.Failure<NightsDetail>("Para realizar una reserva se debe realizar con 24 horas de anticipación");

            return Result.Success(
                new NightsDetail(
                    movementId,
                    DateOnly.FromDateTime(dateStart),
                    DateOnly.FromDateTime(dateEnd),
                    observation,
                    userInSession,
                    DateTime.Now,
                    Status.Active));
        }
    }
}