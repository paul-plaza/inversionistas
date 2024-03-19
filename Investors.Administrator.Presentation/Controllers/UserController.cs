using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Application.Commands.CashBacks;
using Investors.Client.Users.Application.Commands.Invoice;
using Investors.Client.Users.Application.Commands.Nights;
using Investors.Client.Users.Application.Querys.HistoryCashback;
using Investors.Client.Users.Application.Querys.HistoryCashbackDetail;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    /// Controlador de usuarios
    /// </summary>
    [Route("api/users")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController : BaseController
    {
        private readonly ISender _sender;
        private readonly IUserSession _userSession;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="userSession"></param>
        public UserController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
        }

        /// <summary>
        /// Aprueba el movimiento tipo cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/cashback/{movementId:guid}/accept", Name = nameof(AcceptRedeemCashBack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptRedeemCashBack(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemCashBackCommand(userId, movementId, TransactionState.Accepted, _userSession.Id));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Rechaza el movimiento tipo cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/cashback/{movementId:guid}/reject", Name = nameof(RejectRedeemCashBack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectRedeemCashBack(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemCashBackCommand(userId, movementId, TransactionState.Rejected, new Guid(IUserSession.UserVirtualCode)));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Aprueba el movimiento tipo noches
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/nights/{movementId:guid}/accept", Name = nameof(AcceptRedeemNights))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptRedeemNights(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemNightsCommand(userId, movementId, TransactionState.Accepted, new Guid(IUserSession.UserVirtualCode)));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Rechaza el movimiento tipo noches
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{userId:guid}/nights/{movementId:guid}/reject", Name = nameof(RejectRedeemNights))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectRedeemNights(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemNightsCommand(userId, movementId, TransactionState.Rejected, new Guid(IUserSession.UserVirtualCode)));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Consulta lista de cashback por aprobar por operacion y restaurant
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="transactionState"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{operationId:int}/{restaurantId:int}/{transactionState:int}/{date:DateTime}/cashback", Name = nameof(CashbackToApprove))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CashbackToApprove(int operationId, int restaurantId, int transactionState, DateTime date)
        {
            var state = TransactionState.From(transactionState);
            var result = await _sender.Send(new MovementsCashbackQuery(operationId, restaurantId, state, date));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Consulta el detalle de un movimiento de cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cashbackId"></param>
        /// <returns></returns>
        [HttpGet("cashback/{cashbackId:guid}/details", Name = nameof(HistoryCashbackDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HistoryCashbackDetails(Guid cashbackId)
        {
            var result = await _sender.Send(new HistoryCashbackDetailQuery(cashbackId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Consulta lista de noches por aprobar por operacion
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="transactionState"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{operationId:int}/{transactionState:int}/{date:DateTime}/nights", Name = nameof(NightsToApprove))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NightsToApprove(int operationId, int transactionState, DateTime date)
        {
            var state = TransactionState.From(transactionState);
            var result = await _sender.Send(new MovementsNightsQuery(operationId, state, date));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }


        /// <summary>
        /// Registra una factura de un referido para acumular puntos y noches
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost("{userId:guid}/invoices", Name = nameof(RegisterInvoice))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterInvoice(Guid userId, [FromBody] RegisterInvoiceRequest item)
        {
            var result = await _sender.Send(new RegisterInvoiceCommand(userId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Ejecutar proceso de acumulacion de puntos
        /// </summary>
        /// <returns></returns>
        [HttpPost("execute/points", Name = nameof(RegisterPointsInvoice))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterPointsInvoice()
        {
            var result = await _sender.Send(new RegisterPointsInvoiceCommand());
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }



    }
}