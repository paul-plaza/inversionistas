using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Kernel.Shared.Events.Domain.ValueObjects;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Domain.Aggregate
{
    public class Event : BaseEntity<int>
    {
        /// <summary>
        /// Orden de los atributos
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Tipo de item
        /// </summary>
        public ItemType ItemType { get; private set; }

        /// <summary>
        /// Descripción
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<EventDetail> _eventDetails = new();

        /// <summary>
        /// hijos que poseé un evento, evento detalle
        /// </summary>
        public virtual IReadOnlyList<EventDetail> EventDetails => _eventDetails;

        protected Event()
        {

        }

        private Event(
            int order,
            ItemType itemType,
            string description,
            Guid createBy,
            DateTime createOn,
            Status status) : base(createBy, createOn, status)
        {
            Order = order;
            ItemType = itemType;
            Description = description;
        }

        public static Result<Event> Create(
            int order,
            string description,
            Guid createBy)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Event>("La descripción es requerida");
            }

            if (order <= 0)
            {
                return Result.Failure<Event>("El orden es requerido");
            }

            return Result.Success(new Event(order, ItemType.ImageList, description, createBy, DateTime.Now, Status.Active));
        }

        /// <summary>
        /// Actualizar evento
        /// </summary>
        /// <param name="order"></param>
        /// <param name="description"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public Result Update(
            int order,
            string description,
            Guid updateBy)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure("La descripción es requerida");
            }

            if (order <= 0)
            {
                return Result.Failure("El orden es requerido");
            }

            Order = order;
            Description = description;
            base.Update(updateBy);
            return Result.Success();
        }

        /// <summary>
        /// Eliminar evento
        /// </summary>
        /// <param name="userInSession"></param>
        /// <returns></returns>
        public Result DeleteGroup(Guid userInSession)
        {
            _eventDetails.ForEach(x => x.DeleteEventDetail(userInSession));
            base.Delete(userInSession);
            return Result.Success();
        }

        /// <summary>
        /// Agregar evento detalle
        /// </summary>
        /// <param name="eventDetail"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        public Result AddEventDetail(
            EventDetail eventDetail,
            Guid createBy)
        {
            _eventDetails.Add(eventDetail);
            base.Update(createBy);
            return Result.Success();
        }

    }
}