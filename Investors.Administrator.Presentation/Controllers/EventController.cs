using Investors.Kernel.Shared.Events.Application.Commands.Event.Create;
using Investors.Kernel.Shared.Events.Application.Commands.Event.Delete;
using Investors.Kernel.Shared.Events.Application.Commands.Event.Update;
using Investors.Kernel.Shared.Events.Application.Commands.EventDetail;
using Investors.Kernel.Shared.Events.Application.Commands.EventSubDetail;
using Investors.Kernel.Shared.Events.Application.Querys.Admin.EventDetail;
using Investors.Kernel.Shared.Events.Application.Querys.Admin.Events;
using Investors.Kernel.Shared.Events.Application.Querys.ShowEvents;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/events")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class EventController : BaseController
    {
        private readonly IUserSession _userSession;
        private readonly ISender _sender;
        public EventController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
        }

        /// <summary>
        /// Trae todos los eventos para el admin
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetEventsAdmin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventsAdmin()
        {
            var result = await _sender.Send(new EventsForAdminQuery());
            return Ok(result.Value);
        }

        /// <summary>
        /// Crear un grupo de eventos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CreateGroupEvent))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGroupEvent([FromBody] CreateEventRequest item)
        {
            var result = await _sender.Send(new CreateEventCommand(_userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Actualizo grupo de evento por Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{eventId:int}", Name = nameof(UpdateGroupEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGroupEvent(int eventId, [FromBody] UpdateEventRequest item)
        {
            if (eventId != item.Id)
            {
                throw new ArgumentException("El Id del registro debe ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateEventCommand(_userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Elimina un grupo de evento por Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId:int}", Name = nameof(DeleteGroupEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGroupEvent(int eventId)
        {
            var result = await _sender.Send(new DeleteEventCommand(_userSession.Id, eventId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }



        /// <summary>
        /// Obtiene todos los eventos y sus hijos de un grupo seleccionado
        /// </summary>
        /// <returns></returns>
        [HttpGet("{eventId:int}/eventDetails", Name = nameof(GetEventsDetailAndSubDetailAdmin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventsDetailAndSubDetailAdmin(int eventId)
        {
            var result = await _sender.Send(new EventsDetailByIdEventsDetailQuery(eventId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Creo un evento detalle
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("{eventId:int}/eventDetails/", Name = nameof(CreateEventDetail))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEventDetail(int eventId, [FromBody] RegisterEventDetailRequest item)
        {
            var result = await _sender.Send(new RegisterEventDetailCommand(_userSession.Id, eventId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        ///  Actualizo un evento detalle
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventDetailId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{eventId:int}/eventDetails/{eventDetailId:int}", Name = nameof(UpdateEventDetail))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEventDetail(int eventId, int eventDetailId, [FromBody] UpdateEventDetailRequest item)
        {
            if (eventDetailId != item.Id)
            {
                throw new ArgumentException("El Id del registro debe ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateEventDetailCommand(_userSession.Id, eventId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        ///  Elimino un evento detalle
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventDetailId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId:int}/eventDetails/{eventDetailId:int}", Name = nameof(DeleteEventDetail))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventDetail(int eventId, int eventDetailId)
        {
            var result = await _sender.Send(new DeleteEventDetailCommand(_userSession.Id, eventId, eventDetailId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }


        /// <summary>
        ///  Creo un evento sub detalle
        /// </summary>
        /// <param name="eventDetailId"></param>
        /// <param name="item"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPost("{eventId:int}/eventDetails/{eventDetailId:int}/eventSubDetails", Name = nameof(RegisterEventSubDetail))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterEventSubDetail(int eventId, int eventDetailId, [FromBody] RegisterEventSubDetailRequest item)
        {
            var result = await _sender.Send(new RegisterEventSubDetailCommand(_userSession.Id, eventId, eventDetailId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }


        /// <summary>
        ///  Actualizo un evento sub detalle
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventDetailId"></param>
        /// <param name="eventSubDetailId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{eventId:int}/eventDetails/{eventDetailId:int}/eventSubDetails/{eventSubDetailId:int}", Name = nameof(UpdateEventSubDetail))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEventSubDetail(int eventId, int eventDetailId, int eventSubDetailId, [FromBody] UpdateEventSubDetailRequest item)
        {
            if (eventSubDetailId != item.EventSubDetailId)
            {
                throw new ArgumentException("El Id del registro debe ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateEventSubDetailCommand(_userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        ///  Elimino un evento sub detalle
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventDetailId"></param>
        /// <param name="eventSubDetailId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId:int}/eventDetails/{eventDetailId:int}/eventSubDetails/{eventSubDetailId:int}", Name = nameof(DeleteEventSubDetail))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventSubDetail(int eventId, int eventDetailId, int eventSubDetailId)
        {
            var result = await _sender.Send(new DeleteEventSubDetailCommand(_userSession.Id, eventId, eventDetailId, eventSubDetailId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Trae todos los eventos de una operacion seleccionada
        /// </summary>
        /// <returns></returns>
        [HttpGet("operation/{operationId:int}", Name = nameof(GetEvents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEvents(int operationId)
        {
            var result = await _sender.Send(new EventsByOperationIdQuery(operationId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}