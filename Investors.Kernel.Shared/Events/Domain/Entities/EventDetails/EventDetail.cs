using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Domain.Entities.EventSubDetails;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Domain.Entities.EventDetails
{
    public class EventDetail : BaseEntity<int>
    {
        /// <summary>
        /// id del evento
        /// </summary>
        public int EventId { get; private set; }

        /// <summary>
        /// url del logo
        /// </summary>
        public string? UrlLogo { get; private set; }

        /// <summary>
        /// url de imagen a abrir
        /// </summary>
        public string? UrlToOpen { get; private set; }

        /// <summary>
        /// Titulo
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Descripción
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// operación a la que pertenece el evento detalle
        /// </summary>
        public int OperationId { get; private set; }

        /// <summary>
        /// Relación con catálogo
        /// </summary>
        public virtual Event Event { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<EventSubDetail> _eventSubDetails = new();

        /// <summary>
        /// Detalles que poseé el catálogo
        /// </summary>
        public virtual IReadOnlyList<EventSubDetail> EventSubDetails => _eventSubDetails;

        protected EventDetail()
        {

        }

        private EventDetail(
            int operationId,
            int eventId,
            string description,
            string urlLogo,
            string title,
            string urlToOpen,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            EventId = eventId;
            UrlLogo = urlLogo;
            Title = title;
            UrlToOpen = urlToOpen;
            OperationId = operationId;
            Description = description;
        }

        public static Result<EventDetail> Create(
            int operationId,
            int eventId,
            string description,
            string urlLogo,
            string title,
            string urlToOpen,
            Guid createBy)
        {
            if (Errors.Any())
                return Result.Failure<EventDetail>(GetErrors());

            return Result.Success(new EventDetail(
                operationId,
                eventId,
                description,
                urlLogo,
                title,
                urlToOpen,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result UpdateInformation(
            int operationId,
            string title,
            string? urlLogo,
            Guid userInSession)
        {
            Title = title;
            OperationId = operationId;
            UrlLogo = urlLogo;
            Update(userInSession);
            return Result.Success();
        }

        public Result DeleteEventDetail(Guid userInSession)
        {
            _eventSubDetails.ForEach(x => x.DeleteEventSubDetail(userInSession));
            Delete(userInSession);
            return Result.Success();
        }

        public Result AddEventSubDetail(
            EventSubDetail subDetail,
            Guid userInSession)
        {
            _eventSubDetails.Add(subDetail);
            base.Update(userInSession);
            return Result.Success();
        }
    }
}