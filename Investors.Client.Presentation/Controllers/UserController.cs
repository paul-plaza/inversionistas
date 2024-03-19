using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Application.Commands.CashBacks;
using Investors.Client.Users.Application.Commands.Invoice;
using Investors.Client.Users.Application.Commands.Nights;
using Investors.Client.Users.Application.Commands.RegisterUser;
using Investors.Client.Users.Application.Commands.Requests;
using Investors.Client.Users.Application.Commands.UpdateUser;
using Investors.Client.Users.Application.Querys;
using Investors.Client.Users.Application.Querys.HistoryCashback;
using Investors.Client.Users.Application.Querys.HistoryCashbackDetail;
using Investors.Client.Users.Application.Querys.HistoryNights;
using Investors.Client.Users.Application.Querys.UserInvestorById;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Kernel.Shared.Investors.Application.Querys;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Client.Presentation.Controllers
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
        /// Obtiene datos de usuario por identificacion
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(template: "{identification}", Name = nameof(UserInvestorById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserInvestorById(string identification)
        {
            var result = await _sender.Send(new UserInvestorByIdQuery(identification));


            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Registra usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = nameof(RegisterUser))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser()
        {
            var result = await _sender.Send(new RegisterUserCommand(
                UserId: _userSession.Id,
                DisplayName: _userSession.DisplayName,
                Identification: _userSession.Identification,
                Name: _userSession.Name,
                SurName: _userSession.SurName,
                Email: _userSession.Email,
                UserInSession: _userSession.Id));

            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Actualiza el perfil del usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{userId:guid}", Name = nameof(UpdateProfile))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile(Guid userId, [FromBody] UpdateUserRequest item)
        {
            if (userId != item.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }
            var result = await _sender.Send(new UpdateUserCommand(userId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Elimina el perfil del usuario
        /// </summary>
        /// <returns></returns>
        [HttpDelete(Name = nameof(DeleteProfile))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProfile()
        {
            var result = await _sender.Send(new DeleteUserCommand(_userSession.Id));
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
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }
            var result = await _sender.Send(new RegisterInvoiceCommand(userId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Canjear noches
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost("{userId:guid}/nights/redeem", Name = nameof(RedeemNights))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RedeemNights(Guid userId, [FromBody] RegisterRedeemNightsRequest item)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }
            var result = await _sender.Send(new RegisterRedemptionNightsCommand(userId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Consulta el historial de movimientos de nocehs
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/nights/history", Name = nameof(HistoryNightsByUserId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HistoryNightsByUserId(Guid userId)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }

            var result = await _sender.Send(new HistoryNightsByUserIdQuery(userId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Aprueba el movimiento tipo noches
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [AllowAnonymous]
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
        /// Cancela el movimiento tipo noches
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{userId:guid}/nights/{movementId:guid}/cancel", Name = nameof(CancelRedeemNights))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelRedeemNights(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemNightsCommand(userId, movementId, TransactionState.Cancelled, new Guid(IUserSession.UserVirtualCode)));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Canjear cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost("{userId:guid}/cashback/redeem", Name = nameof(RedeemCashBack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RedeemCashBack(Guid userId, [FromBody] ReedemCashBackRequest item)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }
            var result = await _sender.Send(new RegisterRedemptionCashBackCommand(userId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Aprueba el movimiento tipo cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{userId:guid}/cashback/{movementId:guid}/accept", Name = nameof(AcceptRedeemCashBack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptRedeemCashBack(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemCashBackCommand(userId, movementId, TransactionState.Accepted, new Guid(IUserSession.UserVirtualCode)));
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
        [AllowAnonymous]
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
        /// Cancela el movimiento tipo cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movementId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{userId:guid}/cashback/{movementId:guid}/cancel", Name = nameof(CancelRedeemCashBack))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelRedeemCashBack(Guid userId, Guid movementId)
        {
            var result = await _sender.Send(new RedeemCashBackCommand(userId, movementId, TransactionState.Cancelled, new Guid(IUserSession.UserVirtualCode)));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Consulta el historial de movimientos de cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/cashback/history", Name = nameof(HistoryCashbackByUserId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HistoryCashbackByUserId(Guid userId)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }

            var result = await _sender.Send(new HistoryCashbackByUserIdQuery(userId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Consulta el detalle de un movimiento de cashback
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cashbackId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/cashback/{cashbackId:guid}/details", Name = nameof(HistoryCashbackDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HistoryCashbackDetails(Guid userId, Guid cashbackId)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }
            var result = await _sender.Send(new HistoryCashbackDetailQuery(cashbackId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Creo un nuevo movimiento de cashback desde uno existente
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cashbackId"></param>
        /// <returns></returns>
        [HttpGet("{userId:guid}/cashback/{cashbackId:guid}/redeem/from/history", Name = nameof(RedeemCashBackFromHistory))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RedeemCashBackFromHistory(Guid userId, Guid cashbackId)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }

            var result = await _sender.Send(new RedeemCashBackFromHistoryCommand(userId, cashbackId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Consulta notificaciones por usuario
        /// </summary>
        /// <param name="userId">id del usuario</param>
        /// <returns>notificaciones</returns>
        [HttpGet("{userId:guid}/notifications", Name = nameof(GetNotificationsByUserId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNotificationsByUserId(Guid userId)
        {
            if (userId != _userSession.Id)
            {
                throw new ArgumentException("Usuario no permitido");
            }
            var result = await _sender.Send(new NotificationsByUserIdQuery(userId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Lee la notifiacion seleccionada
        /// </summary>
        /// <param name="userId">id del usuario</param>
        /// <param name="notificationId">id de la notificacion</param>
        /// <returns></returns>
        [HttpPut("{userId:guid}/notifications/{notificationId:int}", Name = nameof(ReadNotification))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReadNotification(Guid userId, int notificationId)
        {
            var result = await _sender.Send(new ReadNotificationByIdCommand(userId, notificationId, new Guid(IUserSession.UserVirtualCode)));
            if (result.IsFailure)
                return FromResult(result);

            return Ok(result.Value);
        }

        /// <summary>
        /// Consulta el inversionista por identificaci�n
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        [HttpGet("{identification}/investor", Name = nameof(GetInvestor))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvestor(string identification)
        {
            var result = await _sender.Send(new GetInvestorByIdentificationQuery(identification));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}