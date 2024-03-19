using Investors.Kernel.Shared.Operations.Application.Commands.Menu;
using Investors.Kernel.Shared.Operations.Application.Commands.MenuType;
using Investors.Kernel.Shared.Operations.Application.Commands.Operation;
using Investors.Kernel.Shared.Operations.Application.Commands.Restaurant;
using Investors.Kernel.Shared.Operations.Application.Commands.Room;
using Investors.Kernel.Shared.Operations.Application.Querys.Menu;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Kernel.Shared.Operations.Application.Querys.Restaurant;
using Investors.Kernel.Shared.Operations.Application.Querys.Room;
using Investors.Shared.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investors.Administrator.Presentation.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/operations")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OperationController : BaseController
    {
        private readonly ISender _sender;
        private readonly IUserSession _userSession;

        /// <inheritdoc />
        public OperationController(ISender sender, IUserSession userSession)
        {
            _sender = sender;
            _userSession = userSession;
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
        /// Crea una operacion
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(RegisterOperation))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterOperation([FromBody] RegisterOperationRequest item)
        {
            var result = await _sender.Send(new RegisterOperationCommand(_userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Actualiza una operacion
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{operationId:int}", Name = nameof(UpdateOperation))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOperation(int operationId, [FromBody] UpdateOperationRequest item)
        {
            if (operationId != item.Id)
            {
                throw new ArgumentException("El Id de operación debe ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateOperationCommand(_userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Elimina una operacion
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [HttpDelete("{operationId:int}", Name = nameof(DeleteOperation))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOperation(int operationId)
        {
            var result = await _sender.Send(new DeleteOperationCommand(_userSession.Id, operationId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Trae todos los restaurantes de una operación
        /// </summary>
        /// <returns></returns>
        [HttpGet("{operationId:int}/restaurants", Name = nameof(RestaurantsByOperationId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestaurantsByOperationId(int operationId)
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
        /// Crea un restaurante
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("{operationId:int}/restaurants", Name = nameof(RegisterRestaurant))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterRestaurant(int operationId, [FromBody] RegisterRestaurantRequest item)
        {
            var result = await _sender.Send(new RegisterRestaurantCommand(operationId, _userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Actualiza un restaurante
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{operationId:int}/restaurants/{restaurantId:int}", Name = nameof(UpdateRestaurant))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant(int operationId, int restaurantId, [FromBody] UpdateRestaurantRequest item)
        {
            if (restaurantId != item.Id)
            {
                throw new ArgumentException("El Id de restaurante debe ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateRestaurantCommand(operationId, _userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Elimina un restaurante
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [HttpDelete("{operationId:int}/restaurants/{restaurantId:int}", Name = nameof(DeleteRestaurant))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId, int operationId)
        {
            var result = await _sender.Send(new DeleteRestaurantCommand(_userSession.Id, restaurantId, operationId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }


        /// <summary>
        ///  Crea un tipo de menu
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="item"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpPost("{operationId:int}/restaurants/{restaurantId:int}/menuTypes", Name = nameof(RegisterMenuTypes))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterMenuTypes(int restaurantId, int operationId, [FromBody] RegisterMenuTypesRequest item)
        {
            var result = await _sender.Send(new RegisterMenuTypesCommand(
                UserId: _userSession.Id,
                OperationId: operationId,
                RestaurantId: restaurantId,
                item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Actualiza un tipo de menu
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="menuTypeId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{operationId:int}/restaurants/{restaurantId:int}/menuTypes/{menuTypeId:int}", Name = nameof(UpdateMenuTypes))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMenuTypes(int operationId, int restaurantId, int menuTypeId, [FromBody] UpdateMenuTypesRequest item)
        {
            if (item.Id != menuTypeId)
            {
                throw new ArgumentException("El Id de tipo menú ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateMenuTypesCommand(
                OperationId: operationId,
                RestaurantId: restaurantId,
                UserId: _userSession.Id,
                Item: item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Elimina un tipo de menu
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="operationId"></param>
        /// <param name="menuTypesId"></param>
        /// <returns></returns>
        [HttpDelete("{operationId:int}/restaurants/{restaurantId:int}/menuTypes/{menuTypesId:int}", Name = nameof(DeleteMenuTypes))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMenuTypes(int restaurantId, int operationId, int menuTypesId)
        {
            var result = await _sender.Send(new DeleteMenuTypesCommand(_userSession.Id, restaurantId, operationId, menuTypesId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }


        /// <summary>
        ///Registrar menus
        /// </summary>
        /// <param name="items"></param>
        /// <param name="operationId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="menuTypeId"></param>
        /// <returns></returns>
        [HttpPost("{operationId:int}/restaurants/{restaurantId:int}/menuTypes/{menuTypeId:int}", Name = nameof(RegisterMenus))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterMenus([FromBody] List<MenuRequest> items, int operationId, int restaurantId, int menuTypeId)
        {
            var result = await _sender.Send(new RegisterMenusCommand(_userSession.Id, items, operationId, restaurantId, menuTypeId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Trae todas las habitaciones de una operacion
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

        /// <summary>
        /// Creo una nueva habitacion
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("{operationId:int}/rooms", Name = nameof(RegisterRooms))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterRooms(int operationId, [FromBody] RegisterRoomRequest item)
        {
            var result = await _sender.Send(new RegisterRoomCommand(_userSession.Id, operationId, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Actualiza habitacion
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="roomId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut("{operationId:int}/rooms/{roomId:int}", Name = nameof(UpdateRoom))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRoom(int operationId, int roomId, [FromBody] UpdateRoomRequest item)
        {
            if (roomId != item.Id)
            {
                throw new ArgumentException("El Id de habitación debe ser igual al Id del item a modificar");
            }
            var result = await _sender.Send(new UpdateRoomCommand(operationId, _userSession.Id, item));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }

        /// <summary>
        /// Elimina una habitacion
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="operationId"></param>
        /// <returns></returns>
        [HttpDelete("{operationId:int}/rooms/{roomId:int}", Name = nameof(DeleteRoom))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoom(int roomId, int operationId)
        {
            var result = await _sender.Send(new DeleteRoomCommand(_userSession.Id, roomId, operationId));
            return result.IsSuccess ? Ok(result.Value) : FromResult(result);
        }
    }
}