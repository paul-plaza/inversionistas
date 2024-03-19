using Investors.Kernel.Shared.Catalogs.Application.Querys.Categories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Client.Presentation.Controllers
{
    /// <summary>
    /// Categorias
    /// </summary>
    [Route("api/catalogs")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CatalogController : BaseController
    {
        private readonly ISender _sender;
        /// <inheritdoc />
        public CatalogController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Trae todas las categorias de la aplicacion
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories", Name = nameof(GetCategories))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _sender.Send(new AllCategoriesQuery());
            return Ok(result.Value);
        }
    }
}