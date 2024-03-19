using Investors.Administrator.Options.Application.Querys.GetOptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    /// Controlador de opciones de menu
    /// </summary>
    [Route("api/options")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OptionController : BaseController
    {
        private readonly ISender _sender;
        public OptionController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Obtener todas las opciones de menu
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetAllOptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllOptions()
        {
            var result = await _sender.Send(new GetOptionsQuery());

            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}