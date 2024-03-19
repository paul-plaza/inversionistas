using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.CatalogRoomType;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    /// Controlador de usuarios
    /// </summary>
    [Route("api/catalogs")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CatalogController : BaseController
    {
        private readonly ISender _sender;
        private readonly IUserSession _userSession;

        public CatalogController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
        }

        /// <summary>
        /// Obtiene los tipos de habitaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet("roomsType",Name = nameof(GetRoomsType))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomsType()
        {
            var result = await _sender.Send(new CatalogRoomTypeQuery());
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}