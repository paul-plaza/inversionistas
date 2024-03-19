using Investors.Kernel.Shared.Investors.Application.Commands.InvestorManagement;
using Investors.Kernel.Shared.Investors.Application.Querys.AllInvestors;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    /// Controlador de usuarios
    /// </summary>
    [Route("api/investors")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class InvestorController : BaseController
    {
        private readonly ISender _sender;
        private readonly IUserSession _userSession;

        public InvestorController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
        }



        /// <summary>
        /// Devuelve todos los inversionistas
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetInvestors))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvestors()
        {
            var result = await _sender.Send(new AllInvestorsQuery());
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Administra inversionistas
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(ManagerInvestor))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ManagerInvestor([FromBody] List<InvestorManagementRequest> items)
        {
            var result = await _sender.Send(new InvestorManagementCommand(_userSession.Id, items));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}