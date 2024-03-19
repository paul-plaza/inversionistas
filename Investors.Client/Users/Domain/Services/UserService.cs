using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Repositories;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Repositories;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Categories;
using Investors.Kernel.Shared.Investors.Application.Querys;
using Investors.Kernel.Shared.Operations.Application.Querys.Menu;
using Investors.Kernel.Shared.Operations.Application.Querys.Room;
using MediatR;

namespace Investors.Client.Users.Domain.Services
{
    public class UserService
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly ISender _sender;
        private readonly IReceiptWriteRepository _repositoryReceipt;

        public UserService(IUserWriteRepository userWriteRepository, ISender sender, IReceiptWriteRepository receiptWriteRepository)
        {
            _userWriteRepository = userWriteRepository;
            _sender = sender;
            _repositoryReceipt = receiptWriteRepository;
        }
        public async Task<Result<User>> Register(
            Guid userId,
            string displayName,
            string identification,
            string email,
            string name,
            string surname,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var ensureEmailCreated = Email.Create(email);

            if (ensureEmailCreated.IsFailure)
            {
                return Result.Failure<User>(ensureEmailCreated.Error);
            }

            User userExists = await _userWriteRepository.GetByIdAsync(userId, cancellationToken);
            //si no existe el usuario guardo en base de datos
            if (userExists is not null)
            {
                return Result.Failure<User>("Usuario ya existe no se puede crear nuevamente");
            }

            var isInvestor = await _sender.Send(new GetInvestorByIdentificationQuery(identification), cancellationToken);

            Result<User> user = User.Create(
                id: userId,
                identification: identification,
                displayName: displayName,
                name: name,
                surname: surname,
                userInSession: userInSession,
                email: ensureEmailCreated.Value,
                isInvestor: isInvestor.IsSuccess);

            if (user.IsFailure)
            {
                return user;
            }
            await _userWriteRepository.AddAsync(user.Value, cancellationToken);

            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            return user;
        }

        /// <summary>
        /// Actualiza datos del usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="displayName"></param>
        /// <param name="surname"></param>
        /// <param name="identification"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Result> Update(
            Guid userId,
            string displayName,
            string name,
            string surname,
            string identification,
            CancellationToken cancellationToken)
        {
            User userExists = await _userWriteRepository.GetByIdAsync(userId, cancellationToken);
            if (userExists is null)
            {
                return Result.Failure("Usuario no existe");
            }

            if (identification != userExists.Identification)
            {
                User userActiveExists = await _userWriteRepository
                    .SingleOrDefaultAsync(new UserActiveByIdentificationSpecs(identification), cancellationToken);

                if (userActiveExists is not null)
                {
                    return Result.Failure("Verificar identificación, ya existe un usuario con la misma identificación");
                }
            }

            var isInvestor = await _sender.Send(new GetInvestorByIdentificationQuery(identification), cancellationToken);

            var user = userExists
                .UpdateInformation(
                    name: name,
                    surname: surname,
                    displayName: displayName,
                    identification:
                    identification,
                    isInvestor:
                    isInvestor.IsSuccess,
                    userInSession:
                    userId);
            if (user.IsFailure)
            {
                return Result.Failure(user.Error);
            }
            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            User userExists = await _userWriteRepository.SingleOrDefaultAsync(new UserActiveByIdSpecs(userId), cancellationToken);
            if (userExists is null)
            {
                return Result.Failure("Usuario no existe");
            }
            var user = userExists.DeleteUserAndProfile(userId);
            if (user.IsFailure)
            {
                return Result.Failure(user.Error);
            }
            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<Result<ResponseDefault>> RedeemCashBack(Guid userId, Guid movementId, Guid userInSession, TransactionState transactionState, CancellationToken cancellationToken)
        {
            var user = await _userWriteRepository.SingleOrDefaultAsync(new UserActiveAndMovementsSpecs(userId, movementId, TransactionType.Cashback), cancellationToken);
            if (user == null)
            {
                return Result.Failure<ResponseDefault>("Usuario o movimiento no encontrado");
            }
            string mensaje = "Se ha rechazado el pedido del inversionista con éxito";
            var ensuredRedeem = user.UpdateMovementCashback(transactionState, userInSession);

            if (ensuredRedeem.IsFailure)
                return Result.Failure<ResponseDefault>(ensuredRedeem.Error);

            if (transactionState == TransactionState.Accepted)
                mensaje = "Se ha aceptado el pedido del inversionista con éxito";

            if (transactionState == TransactionState.Cancelled)
                mensaje = "Has cancelado tu pedido exitosamente";

            //agregar movimientos (detalle de consumos)
            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault(mensaje);
            return Result.Success(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="guestIdentification"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> RegisterReceipt(Guid userId, string guestIdentification, DateTime invoiceDate, CancellationToken cancellationToken)
        {
            User userExists = await _userWriteRepository.GetByIdAsync(userId, cancellationToken);
            if (userExists is null)
            {
                return Result.Failure<ResponseDefault>("Usuario no existe");
            }
            if (userExists.Status != Status.Active)
            {
                return Result.Failure<ResponseDefault>("Usuario no se encuentra activo");
            }
            //obtengo un registro por identificacion y fecha de factura
            var receipt = await _repositoryReceipt.SingleOrDefaultAsync(new ReceiptsByIdentificacionAndDateSpecs(guestIdentification, DateOnly.FromDateTime(invoiceDate)), cancellationToken);


            if (receipt is null)
            {
                var response = userExists.RegisterReceipt(guestIdentification, DateOnly.FromDateTime(invoiceDate), userId);
                if (response.IsFailure)
                    return Result.Failure<ResponseDefault>(response.Error);
            }

            if (receipt is not null)
            {
                var response = receipt.ReSyncReceipt(userId);
                if (response.IsFailure)
                    return Result.Failure<ResponseDefault>(response.Error);
            }


            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result =
                new ResponseDefault("Su factura se ha agregado con éxito y será validada por un administrador, en las proximas 24 horas sus puntos y noches serán cargados a su cuenta.");
            return Result.Success(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="movementId"></param>
        /// <param name="transactionState"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> RedeemNights(Guid userId, Guid movementId, TransactionState transactionState, Guid userInSession, CancellationToken cancellationToken)
        {
            var user = await _userWriteRepository.SingleOrDefaultAsync(new UserActiveAndMovementsSpecs(userId, movementId, TransactionType.Nights), cancellationToken);
            if (user == null)
            {
                return Result.Failure<ResponseDefault>("Usuario no encontrado");
            }
            string mensaje = "Se ha rechazado el pedido del inversionista con éxito";
            var ensuredRedeem = user.UpdateMovementNights(transactionState, userInSession);

            if (ensuredRedeem.IsFailure)
                return Result.Failure<ResponseDefault>(ensuredRedeem.Error);

            if (transactionState == TransactionState.Accepted)
                mensaje = "Se ha aceptado el pedido del inversionista con éxito";

            if (transactionState == TransactionState.Cancelled)
                mensaje = "Has cancelado tu pedido exitosamente";

            //agregar movimientos (detalle de consumos)
            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault(mensaje);
            return Result.Success(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="menusId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        /// <param name="userInSession"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> RegisterRedemptionCashback(
            int operationId,
            IEnumerable<int> menusId,
            int restaurantId,
            Guid userId,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var userExists = await _userWriteRepository.SingleOrDefaultAsync(new UserActiveByIdSpecs(userId), cancellationToken);
            if (userExists is null)
            {
                return Result.Failure<ResponseDefault>("Usuario no existe");
            }

            if (userExists.UserType == UserType.Invited)
            {
                return Result.Failure<ResponseDefault>("Solo usuarios inversionistas pueden redimir puntos");
            }

            var menusByRestaurant = await _sender.Send(new MenuByOperationIdAndRestaurantIdQuery(operationId, restaurantId), cancellationToken);
            if (menusByRestaurant.IsFailure)
            {
                return Result.Failure<ResponseDefault>("Menú en restaurante seleccionado no existe");
            }

            var menus = menusByRestaurant.Value
                .SelectMany(x => x.Menus)
                .Where(x => menusId.Contains(x.Id)).ToList();
            //verifico que existan la misma cantidad de menus seleccionados que los enviados
            if (menus.Count != menusId.Distinct().Count())
            {
                return Result.Failure<ResponseDefault>("Al menos un menú seleccionado no existe");
            }

            var points = menusId
                .Select(x =>
                    menus.Single(y => y.Id == x)).Sum(x => x.Points);
            //Valido que tenga puntos suficientes para redimir
            if (userExists.Profile.CashBackToRedeem < points)
                return Result.Failure<ResponseDefault>("No cuentas con puntos suficientes para realizar ésta operación");

            //transformo la lista de id en menuResponse
            var menusSelected = menusId
                .Select(x =>
                    menus.Single(y => y.Id == x))
                .ToList();

            var response = userExists.RegisterRedemptionCashback(userId, userExists.Email, userExists.Identification, operationId, menusSelected, restaurantId, userInSession);
            if (response.IsFailure)
                return Result.Failure<ResponseDefault>(response.Error);


            await _userWriteRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(
                new ResponseDefault("Nos complace informarle que su pedido ha sido registrado satisfactoriamente. Ahora, por favor diríjase a la caja para finalizar su pedido."));
        }

        public async Task<Result<ResponseDefault>> RegisterRedemptionNights(
            Guid userId,
            int operationId,
            string? observation,
            int roomId,
            DateTime dateStart,
            DateTime dateEnd,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var userExists = await _userWriteRepository.SingleOrDefaultAsync(new UserActiveByIdSpecs(userId), cancellationToken);
            if (userExists is null)
            {
                return Result.Failure<ResponseDefault>("Usuario no existe");
            }

            if (userExists.UserType == UserType.Invited)
            {
                return Result.Failure<ResponseDefault>("Solo usuarios inversionistas pueden redimir noches");
            }

            var roomsByOperationId = await _sender.Send(new RoomsByOperationIdQuery(operationId), cancellationToken);
            if (roomsByOperationId.IsFailure)
            {
                return Result.Failure<ResponseDefault>("No se pudo encontrar habitaciones para la operación seleccionada");
            }

            var room = roomsByOperationId.Value.FirstOrDefault(x => x.Id == roomId);

            if (room is null)
                return Result.Failure<ResponseDefault>("No se encontró habitación");

            var response = userExists.RegisterRedemptionNights(userId, userExists.Email, userExists.Identification, operationId, observation, roomId, dateStart, dateEnd, userInSession);
            if (response.IsFailure)
                return Result.Failure<ResponseDefault>(response.Error);

            await _userWriteRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(
                new ResponseDefault("Nos complace informarle que su pedido ha sido registrado satisfactoriamente. Ahora, por favor espere el mensaje de confirmación."));
        }

        public async Task<Result> RegisterNotification(
            Guid userId,
            string title,
            string? subTitle,
            string description,
            NotificationType notificationType,
            int? operationId,
            Guid userInSession,
            CancellationToken cancellationToken)
        {
            var user = await _userWriteRepository.SingleOrDefaultAsync(new UserActiveByIdSpecs(userId), cancellationToken);
            if (user == null)
            {
                return Result.Failure("Usuario no encontrado");
            }
            var notification = user.RegisterNotification(userId, title, subTitle, description, notificationType, operationId, userInSession);
            if (notification.IsFailure)
                return Result.Failure(notification.Error);

            await _userWriteRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        /// <summary>
        /// Lee la notificación seleccionada
        /// </summary>
        /// <param name="userId">id de usuario</param>
        /// <param name="notificationtId">id de notificación a afectar</param>
        /// <param name="userInSession">usuario en sesion</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ResponseDefault>> ReadNotification(Guid userId, int notificationtId, Guid userInSession, CancellationToken cancellationToken)
        {
            var user = await _userWriteRepository.SingleOrDefaultAsync(new UserAndNotificationByUserIdSpecs(userId), cancellationToken);
            if (user == null)
            {
                return Result.Failure<ResponseDefault>("Usuario o notificacion no encontrado");
            }

            var notification = user.Notifications.FirstOrDefault(notification => notification.Id == notificationtId);
            if (notification == null)
            {
                return Result.Failure<ResponseDefault>("Notificacion no encontrada");
            }
            var ensuredRedeem = user.ReadNotification(Status.Inactive, notificationtId, userInSession);

            if (ensuredRedeem.IsFailure)
                return Result.Failure<ResponseDefault>(ensuredRedeem.Error);

            await _userWriteRepository.SaveChangesAsync(cancellationToken);
            ResponseDefault result = new ResponseDefault("Notificación se ha leido con éxito");
            return Result.Success(result);
        }

        public async Task<Result> UpdateCategoryUser(User user, IEnumerable<CategoryResponse> categories, bool isInvestor, CancellationToken cancellationToken)
        {
            user.UpdateCategory(user, categories, isInvestor);
            var result = await _userWriteRepository.SaveChangesAsync(cancellationToken);
            return Result.Success(result);
        }
    }
}