using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Kernel.Shared.Events.Domain.Entities.EventSubDetails;
using Investors.Kernel.Shared.Events.Repositories;
using Investors.Kernel.Shared.Events.Specifications;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Events.Domain.Services
{
    public class EventDetailService
    {
        private readonly IEventWriteRepository _eventRepository;
        private readonly ISender _sender;

        public EventDetailService(IEventWriteRepository eventRepository, ISender sender)
        {
            _eventRepository = eventRepository;
            _sender = sender;
        }

        /// <summary>
        /// Crear un evento
        /// </summary>
        /// <param name="description"></param>
        /// <param name="order"></param>
        /// <param name="createBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> CreateEvent(
            string description,
            int order,
            Guid createBy,
            CancellationToken cancellationToken)
        {
            Result<Event> ensureCreateEvent = Event.Create(
                order: order,
                description,
                createBy);

            if (ensureCreateEvent.IsFailure)
            {
                return Result.Failure<ResponseDefault>(ensureCreateEvent.Error);
            }
            await _eventRepository.AddAsync(ensureCreateEvent.Value, cancellationToken);
            await _eventRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }

        /// <summary>
        /// Actualizar un evento
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="description"></param>
        /// <param name="order"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> UpdateEvent(
            int eventId,
            string description,
            int order,
            Guid userInSession,
            CancellationToken cancellationToken)
        {

            Event? eventExist = await _eventRepository.GetByIdAsync(eventId, cancellationToken);

            if (eventExist is null)
            {
                return Result.Failure<ResponseDefault>("No existe un Grupo principal para agregar el evento");
            }

            eventExist.Update(order, description, userInSession);
            await _eventRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Grupo actualizado con éxito");
            return Result.Success(result);
        }

        /// <summary>
        /// Eliminar un evento
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> DeleteEvent(
            int eventId,
            Guid userInSession,
            CancellationToken cancellationToken)
        {

            Event? eventExist = await _eventRepository.GetByIdAsync(eventId, cancellationToken);

            if (eventExist is null)
            {
                return Result.Failure<ResponseDefault>("No existe un Grupo principal para agregar el evento");
            }

            eventExist.DeleteGroup(userInSession);
            await _eventRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Grupo eliminado con éxito");
            return Result.Success(result);
        }


        /// <summary>
        /// Agrego un detalle al grupo
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="eventId"></param>
        /// <param name="description"></param>
        /// <param name="urlLogo"></param>
        /// <param name="title"></param>
        /// <param name="urlToOpen"></param>
        /// <param name="createBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> AddEventDetail(
            int operationId,
            int eventId,
            string? description,
            string? urlLogo,
            string? title,
            string? urlToOpen,
            Guid createBy,
            CancellationToken cancellationToken)
        {

            Event? eventExist = await _eventRepository.GetByIdAsync(eventId, cancellationToken);

            if (eventExist is null)
            {
                return Result.Failure<ResponseDefault>("No existe un Grupo principal para agregar el evento");
            }

            Result<EventDetail> eventDetail = EventDetail.Create(
                operationId,
                eventId,
                description,
                urlLogo,
                title,
                urlToOpen,
                createBy);

            if (eventDetail.IsFailure)
            {
                return Result.Failure<ResponseDefault>(eventDetail.Error);
            }

            var ensureAddEventDetail = eventExist.AddEventDetail(
                eventDetail.Value,
                createBy: createBy
                );

            if (ensureAddEventDetail.IsFailure)
            {
                return Result.Failure<ResponseDefault>(ensureAddEventDetail.Error);
            }

            await _eventRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }

        /// <summary>
        /// Actualizar evento detalle
        /// </summary>
        /// <param name="eventDetailId"></param>
        /// <param name="eventId"></param>
        /// <param name="operationId"></param>
        /// <param name="title"></param>
        /// <param name="urlLogo"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> UpdateEventDetail(
            int eventDetailId,
            int eventId,
            int operationId,
            string title,
            string? urlLogo,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            Event? eventGroup = await _eventRepository.SingleOrDefaultAsync(new GetEventIncludeDetailByIdSpecs(eventId, eventDetailId), cancellationToken);
            if (eventGroup is null)
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            if (!eventGroup.EventDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            var ensureUpdateDetail = eventGroup.EventDetails[0].UpdateInformation(
                operationId: operationId,
                title: title,
                urlLogo: urlLogo,
                userInSession: userInSession
                );

            if (ensureUpdateDetail.IsFailure)
            {
                return Result.Failure<ResponseDefault>(ensureUpdateDetail.Error);
            }

            await _eventRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }

        /// <summary>
        /// Elimina evento detalle
        /// </summary>
        /// <param name="eventDetailId"></param>
        /// <param name="eventId"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> DeleteEventDetail(
            int eventDetailId,
            int eventId,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            Event? eventGroup = await _eventRepository.SingleOrDefaultAsync(new GetEventIncludeDetailByIdSpecs(eventId, eventDetailId), cancellationToken);
            if (eventGroup is null)
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            if (!eventGroup.EventDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            var ensureUpdateDetail = eventGroup.EventDetails[0].DeleteEventDetail(userInSession: userInSession);

            if (ensureUpdateDetail.IsFailure)
            {
                return Result.Failure<ResponseDefault>(ensureUpdateDetail.Error);
            }

            await _eventRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }

        /// <summary>
        /// Crea un evento sub detalle
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventDetailId"></param>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="image"></param>
        /// <param name="createBy"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> AddEventSubDetail(
            int eventId,
            int eventDetailId,
            string? description,
            string? title,
            string? image,
            Guid createBy,
            CancellationToken cancellationToken)
        {
            Event? eventGroup = await _eventRepository.SingleOrDefaultAsync(new GetEventIncludeDetailByIdSpecs(eventId, eventDetailId), cancellationToken);
            if (eventGroup is null)
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            if (!eventGroup.EventDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            Result<EventSubDetail> ensureCreateSubDetail = EventSubDetail.Create(
                eventDetailId,
                title,
                description,
                image,
                createBy);

            if (ensureCreateSubDetail.IsFailure)
            {
                return Result.Failure<ResponseDefault>(ensureCreateSubDetail.Error);
            }

            var ensureUpdateDetail = eventGroup.EventDetails[0].AddEventSubDetail(
                ensureCreateSubDetail.Value,
                userInSession: createBy);

            if (ensureUpdateDetail.IsFailure)
            {
                return Result.Failure<ResponseDefault>(ensureUpdateDetail.Error);
            }

            await _eventRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> UpdateEventSubDetail(
            int eventId,
            int eventDetailId,
            int eventSubDetailId,
            string description,
            string title,
            string image,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var eventGroup = await _eventRepository
                .SingleOrDefaultAsync(new GetEventIncludeDetailAndSubDetailByIdSpecs(eventId, eventDetailId, eventSubDetailId), cancellationToken);
            if (eventGroup is null)
            {
                return Result.Failure<ResponseDefault>("No existe padre del registro");
            }

            if (!eventGroup.EventDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe evento detalle");
            }

            if (!eventGroup.EventDetails[0].EventSubDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }


            var response = eventGroup.EventDetails[0].EventSubDetails[0]
                .UpdateInformation(eventDetailId, title, description, image, userInSession);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Error al actualizar registro");
            }
            await _eventRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> DeleteEventSubDetail(
            int eventId,
            int eventDetailId,
            int eventSubDetailId,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var eventGroup = await _eventRepository
                .SingleOrDefaultAsync(new GetEventIncludeDetailAndSubDetailByIdSpecs(eventId, eventDetailId, eventSubDetailId), cancellationToken);
            if (eventGroup is null)
            {
                return Result.Failure<ResponseDefault>("No existe padre del registro");
            }

            if (!eventGroup.EventDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe evento detalle");
            }

            if (!eventGroup.EventDetails[0].EventSubDetails.Any())
            {
                return Result.Failure<ResponseDefault>("No existe registro");
            }

            var response = eventGroup.EventDetails[0].EventSubDetails[0].DeleteEventSubDetail(userId);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _eventRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha eliminado con éxito");
            return Result.Success(result);
        }
    }
}