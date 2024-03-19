using Investors.Kernel.Shared.Operations.Application.Querys.Menu;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Kernel.Shared.Operations.Application.Querys.Restaurant;
using Investors.Kernel.Shared.Operations.Application.Querys.Room;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Client.Presentation.Controllers
{
    [Route("api/operations")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OperationController : BaseController
    {
        private readonly ISender _sender;

        public OperationController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Trae todas las operaciones.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetOperations))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOperations()
        {
            var result = await _sender.Send(new GetOperationsQuery());
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Trae todos los restauntates de una operación
        /// </summary>
        /// <returns></returns>
        [HttpGet("{operationId:int}/restaurants", Name = nameof(GetOperationById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOperationById(int operationId)
        {
            var result = await _sender.Send(new RestaurantsByOperationIdQuery(operationId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Trae un restaurante por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{operationId:int}/restaurants/{restaurantId:int}", Name = nameof(GetTypeMenusByRestaurant))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTypeMenusByRestaurant(int operationId, int restaurantId)
        {
            var result = await _sender.Send(new MenuByOperationIdAndRestaurantIdQuery(operationId, restaurantId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Trae todas las habitaciones de una operación
        /// </summary>
        /// <returns></returns>
        [HttpGet("{operationId:int}/rooms", Name = nameof(GetRoomsByIdOperation))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomsByIdOperation(int operationId)
        {
            var result = await _sender.Send(new RoomsByOperationIdQuery(operationId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}