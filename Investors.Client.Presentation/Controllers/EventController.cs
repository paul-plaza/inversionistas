using Investors.Kernel.Shared.Events.Application.Querys;
using Investors.Kernel.Shared.Events.Application.Querys.ShowEvents;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Client.Presentation.Controllers
{
    [Route("api/events")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class EventController : BaseController
    {
        private readonly ISender _sender;
        private readonly IUserSession _userSession;
        public EventController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
        }

        /// <summary>
        /// Trae todos los eventos y sus hijos del home.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetEvents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEvents()
        {

            var result = await _sender.Send(new EventsQuery(_userSession.Identification));
            return Ok(result.Value);
        }
    }
}