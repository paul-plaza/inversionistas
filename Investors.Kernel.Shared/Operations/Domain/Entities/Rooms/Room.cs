using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Domain.Entities.Rooms
{
    public class Room : BaseEntity<int>
    {
        public const int ObservationMaxLength = 1000;

        public const int DescriptionMaxLength = 250;

        public const int TitleMaxLength = 250;

        /// <summary>
        /// Descripción de habitación
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Titulo del habitación
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// url del logo de habitación
        /// </summary>
        public string UrlLogo { get; private set; }

        /// <summary>
        /// Observación que se muestra en pantalla
        /// </summary>
        public string Observation { get; private set; }

        /// <summary>
        /// Número máximo de huéspedes
        /// </summary>
        public int Guests { get; private set; }


        // public virtual Operation Operation { get; private set; }

        /// <summary>
        /// Tipo de habitación
        /// </summary>
        public int RoomTypeId { get; private set; }

        /// <summary>
        /// Relación con operación
        /// </summary>
        public virtual Operation Operation { get; private set; }

        /// <summary>
        /// Id de Operación
        /// </summary>
        public int OperationId { get; private set; }


        protected Room()
        {

        }

        private Room(
            int operationId,
            string description,
            string title,
            string urlLogo,
            int guests,
            int roomTypeId,
            string observation,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            OperationId = operationId;
            Description = description;
            Title = title;
            UrlLogo = urlLogo;
            Guests = guests;
            RoomTypeId = roomTypeId;
            Observation = observation;
        }

        public static Result<Room> Create(
            int operationId,
            string description,
            string title,
            string urlLogo,
            int guests,
            int roomType,
            string observation,
            Guid createBy)
        {
            Guard.Against.NullOrWhiteSpace(description);
            Guard.Against.NullOrWhiteSpace(urlLogo);
            Guard.Against.NullOrWhiteSpace(title);
            Guard.Against.NullOrWhiteSpace(observation);

            if (title.Length > TitleMaxLength)
                Errors.Add(ValidationConstants.ValidateMaxLength("Titulo", TitleMaxLength));

            if (description.Length > DescriptionMaxLength)
                Errors.Add(ValidationConstants.ValidateMaxLength("Descripción", DescriptionMaxLength));

            if (observation.Length > ObservationMaxLength)
                Errors.Add(ValidationConstants.ValidateMaxLength("Observación", ObservationMaxLength));

            if (Errors.Any())
                return Result.Failure<Room>(GetErrors());

            return Result.Success(new Room(
                operationId,
                description,
                title,
                urlLogo,
                guests,
                roomType,
                observation,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result UpdateInformation(
            string description,
            string title,
            string urlLogo,
            int guests,
            int roomType,
            string observation,
            Guid userInSession)
        {
            Description = description;
            Title = title;
            UrlLogo = urlLogo;
            Guests = guests;
            RoomTypeId = roomType;
            Observation = observation;
            Update(userInSession);
            return Result.Success();
        }

        public Result DeleteRoom(Guid userId)
        {
            Delete(userId);
            return Result.Success();
        }
    }
}