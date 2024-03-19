using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Domain.Entities.EventSubDetails
{
    public class EventSubDetail : BaseEntity<int>
    {
        /// <summary>
        /// Titulo
        /// </summary>
        public string? Title { get; private set; }

        /// <summary>
        /// Descripción
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// url de imagen 
        /// </summary>
        public string? Image { get; private set; }

        /// <summary>
        /// Relación con evento detalle
        /// </summary>
        public virtual EventDetail EventDetail { get; private set; }

        /// <summary>
        /// Id de beneficio
        /// </summary>
        public int EventDetailId { get; private set; }

        protected EventSubDetail()
        {

        }

        private EventSubDetail(
            int eventDetailId,
            string? title,
            string? description,
            string? image,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            EventDetailId = eventDetailId;
            Title = title;
            Description = description;
            Image = image;
        }

        public static Result<EventSubDetail> Create(
            int eventDetailId,
            string? title,
            string? description,
            string? image,
            Guid createBy)
        {
            if (Errors.Any())
                return Result.Failure<EventSubDetail>(GetErrors());

            return Result.Success(new EventSubDetail(
                eventDetailId,
                title,
                description,
                image,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result UpdateInformation(
            int eventDetailId,
            string title,
            string description,
            string image,
            Guid userInSession)
        {
            EventDetailId = eventDetailId;
            Title = title;
            Description = description;
            Image = image;

            Update(userInSession);
            return Result.Success();
        }

        public Result DeleteEventSubDetail(Guid userId)
        {
            Delete(userId);
            return Result.Success();
        }
    }
}