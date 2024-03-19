using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Application.Commands.Menu;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Kernel.Shared.Operations.Domain.Repositories;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Shared.Domain;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Domain.Services
{
    public class OperationService
    {
        private readonly IOperationWriteRepository _operationRepository;
        private readonly ISender _sender;
        public OperationService(IOperationWriteRepository operationRepository, ISender sender)
        {
            _operationRepository = operationRepository;
            _sender = sender;
        }

        public async Task<Result<ResponseDefault>> Register(
            int order,
            string description,
            string alias,
            string urlLogo,
            Guid idUserCreate,
            string userInvoice,
            string passwordInvoice,
            string email,
            CancellationToken cancellationToken)
        {
            Result<Operation> operation = Operation.Create(
                order,
                description,
                alias,
                urlLogo,
                idUserCreate,
                userInvoice,
                passwordInvoice,
                email);

            if (operation.IsFailure)
            {
                return Result.Failure<ResponseDefault>(operation.Error);
            }
            await _operationRepository.AddAsync(operation.Value, cancellationToken);
            await _operationRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }


        public async Task<Result<ResponseDefault>> Update(
            int id,
            int order,
            string description,
            string alias,
            string urlLogo,
            string userInvoice,
            string passwordInvoice,
            string email,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            Operation operationExists = await _operationRepository.GetByIdAsync(id, cancellationToken);
            if (operationExists is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            var response = operationExists.UpdateInformation(order, description, alias, urlLogo, userInvoice, passwordInvoice, email, userInSession);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Error al actualizar la operación");
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }


        public async Task<Result<ResponseDefault>> DeleteOperation(int idOperation, Guid userId, CancellationToken cancellationToken)
        {
            var operationExists = await _operationRepository.GetByIdAsync(idOperation, cancellationToken);
            if (operationExists is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            var response = operationExists.DeleteOperation(userId);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha eliminado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> RegisterRestaurant(
            int operationId,
            string description,
            string urlLogo,
            string email,
            Guid createBy,
            CancellationToken cancellationToken)
        {
            Operation operationExists = await _operationRepository.GetByIdAsync(operationId, cancellationToken);
            if (operationExists is null)
            {
                return Result.Failure<ResponseDefault>("Error al consultar operaciones");

                ;
            }
            //TODO: esperar definicion de indices para restaurantes
            var response = operationExists.RegisterNewRestaurant(operationId, description, urlLogo, email, createBy);

            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> UpdateRestaurant(
            int id,
            int operationId,
            string description,
            string urlLogo,
            string email,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.SingleOrDefaultAsync(new RestaurantByIdSpecs(id, operationId), cancellationToken);
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("EL registro no existe");
            }
            var restaurant = operation.Restaurants.SingleOrDefault();
            if (restaurant is null)
            {
                return Result.Failure<ResponseDefault>("No existe restaurant");
            }
            var response = restaurant.UpdateInformation(operationId, description, urlLogo, email, userInSession);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Error al actualizar restaurant");
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> DeleteRestaurant(int idRestaurant, Guid userId, int operationId, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.SingleOrDefaultAsync(new RestaurantByIdSpecs(idRestaurant, operationId), cancellationToken);
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            var restaurant = operation.Restaurants.SingleOrDefault();
            if (restaurant is null)
            {
                return Result.Failure<ResponseDefault>("No existe restaurant");
            }
            var response = restaurant.DeleteRestaurant(userId);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha eliminado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> RegisterMenus(IEnumerable<MenuRequest> menus,
            int operationId,
            int restaurantId,
            int menuTypeId,
            Guid userId,
            CancellationToken cancellation)
        {
            var operation = await _operationRepository
                .SingleOrDefaultAsync(new OperationAndRestaurantByDescriptionSpecs(operationId, restaurantId, menuTypeId), cancellation);
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            if (!operation.Restaurants.Any())
            {
                return Result.Failure<ResponseDefault>("No existe restaurant");
            }

            if (!operation.Restaurants[0].MenuTypes.Any())
            {
                return Result.Failure<ResponseDefault>("No existe categoría de menú seleccionado");
            }

            //pongo a todos en estado eliminado para luego actualizarlos
            operation.Restaurants[0].MenuTypes[0].DeleteAllMenu(userId);

            //obtengo todos los menus asociados al tipo de menu para actualizarlos
            var menusToUpdate =
                from menuDb in operation.Restaurants[0].MenuTypes[0].Menus
                join menuDoc in menus
                    on menuDb.Id equals menuDoc.Id
                select menuDb.UpdateInformation(menuDoc.Title, menuDoc.Description, menuDoc.Points, userId);

            if (menusToUpdate.Any(x => x.IsFailure))
            {
                return Result.Failure<ResponseDefault>("Existe un error en la actualización de los menús, revise la información enviada");
            }

            //creo los nuevos menus
            var menusToInsert =
                menus.Where(x => x.Id == 0)
                    .Select(x => Menu.Create(x.Description, x.Title, x.Points, userId))
                    .ToList();

            if (menusToInsert.Any(x => x.IsFailure))
            {
                return Result.Failure<ResponseDefault>("Existe un error en la creación de los menús, revise la información enviada");
            }

            operation.Restaurants[0].MenuTypes[0].RegisterNewMenus(menusToInsert.Select(menu => menu.Value).ToList());

            await _operationRepository.SaveChangesAsync(cancellation);
            ResponseDefault result = new ResponseDefault("Sus registros se han sincronizado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> RegisterRoom(
            int operationId,
            string description,
            string title,
            string urlLogo,
            int guest,
            int roomType,
            string observation,
            Guid createBy,
            CancellationToken cancellationToken)
        {
            var operations = await _operationRepository
                .SingleOrDefaultAsync(new OperationIncludeRoomsByOperationIdSpecs(operationId), cancellationToken);

            if (operations is null)
            {
                return Result.Failure<ResponseDefault>("No se encontro la operación");
            }

            var response = operations.RegisterNewRoom(
                operationId,
                description,
                title,
                urlLogo,
                guest,
                roomType,
                observation,
                createBy);

            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);

            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }

        /// <summary>
        /// Actualiza una habitacion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationId"></param>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="urlLogo"></param>
        /// <param name="observation"></param>
        /// <param name="guest"></param>
        /// <param name="roomType"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> UpdateRoom(
            int id,
            int operationId,
            string description,
            string title,
            string urlLogo,
            string observation,
            int guest,
            int roomType,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var operation = await _operationRepository
                .SingleOrDefaultAsync(
                    new OperationIncludeRoomByIdSpecs(
                        roomId: id,
                        operationId: operationId), cancellationToken);
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            var room = operation.Rooms.SingleOrDefault();
            if (room is null)
            {
                return Result.Failure<ResponseDefault>("No existe habitación");
            }
            var response = room
                .UpdateInformation(
                    description,
                    title,
                    urlLogo,
                    guest,
                    roomType,
                    observation,
                    userInSession);

            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Error al actualizar habitación");
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> DeleteRoom(int idRoom, Guid userId, int operationId, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.SingleOrDefaultAsync(new OperationIncludeRoomByIdSpecs(idRoom, operationId));
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            var room = operation.Rooms.SingleOrDefault();
            if (room is null)
            {
                return Result.Failure<ResponseDefault>("No existe habitación");
            }
            var response = room.DeleteRoom(userId);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha eliminado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> RegisterMenuTypes(
            int operationId,
            int restaurantId,
            string description,
            string urlLogo,
            Guid createBy,
            CancellationToken cancellationToken)
        {
            Operation? operationExists = await _operationRepository.SingleOrDefaultAsync(new RestaurantByIdSpecs(restaurantId, operationId), cancellationToken);
            if (operationExists is null)
            {
                return Result.Failure<ResponseDefault>("Operación no existe");
            }

            if (!operationExists.Restaurants.Any())
            {
                return Result.Failure<ResponseDefault>("Restaurante no existe");
            }

            var response = operationExists.Restaurants[0].RegisterNewMenuType(
                description: description,
                urlLogo: urlLogo,
                createBy: createBy
                );

            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>(response.Error);
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha guardado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> UpdateMenuTypes(
            int menuTypeId,
            int operationId,
            int restaurantId,
            string description,
            string urlLogo,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var operation = await _operationRepository
                .SingleOrDefaultAsync(new RestaurantIncludeMenuTypeByIdSpecs(restaurantId, operationId, menuTypeId), cancellationToken);
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            if (!operation.Restaurants.Any())
            {
                return Result.Failure<ResponseDefault>("No existe restaurant");
            }

            if (!operation.Restaurants[0].MenuTypes.Any())
            {
                return Result.Failure<ResponseDefault>("No existe categoría de menú seleccionado");
            }

            var response = operation.Restaurants[0].MenuTypes[0].UpdateInformation(description, urlLogo, userInSession);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Error al actualizar tipo menú");
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha actualizado con éxito");
            return Result.Success(result);
        }

        public async Task<Result<ResponseDefault>> DeleteMenuTypes(int restaurantId, Guid userId, int operationId, int menuTypeId, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository
                .SingleOrDefaultAsync(new RestaurantIncludeMenuTypeByIdSpecs(restaurantId, operationId, menuTypeId), cancellationToken);
            if (operation is null)
            {
                return Result.Failure<ResponseDefault>("No existe operación");
            }
            if (!operation.Restaurants.Any())
            {
                return Result.Failure<ResponseDefault>("No existe restaurant");
            }

            if (!operation.Restaurants[0].MenuTypes.Any())
            {
                return Result.Failure<ResponseDefault>("No existe categoría de menú seleccionado");
            }

            var response = operation.Restaurants[0].MenuTypes[0].DeleteMenuType(userId);
            if (response.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Error al actualizar tipo menú");
            }
            await _operationRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Su registro se ha eliminado con éxito");
            return Result.Success(result);
        }
    }
}