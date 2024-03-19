using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Investors.Domain.Entities;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Domain.Aggregate
{
    public class Operation : BaseEntity<int>
    {
        /// <summary>
        /// Orden de la operación
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Descripción de operación
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Alias de la operación
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// url del logo de la operación
        /// </summary>
        public string UrlLogo { get; private set; }

        /// <summary>
        /// Nombre de usuario de la operación para factura con satcom
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Contraseña de usuario de la operación para factura con satcom
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Correo del encargado para enviar notificaciones
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<Restaurant> _restaurants = new();

        /// <summary>
        /// Restaurantes que posee la operación
        /// </summary>
        public virtual IReadOnlyList<Restaurant> Restaurants => _restaurants;

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<Room> _rooms = new();

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<InvestorOperation> _investorOperations = new();

        /// <summary>
        /// Detalle de factura
        /// </summary>
        public virtual IReadOnlyList<InvestorOperation> InvestorOperations => _investorOperations;

        /// <summary>
        /// Habitaciones que posee la operación
        /// </summary>
        public virtual IReadOnlyList<Room> Rooms => _rooms;

        protected Operation()
        {
        }

        private Operation(
            int order,
            string description,
            string alias,
            string urlLogo,
            string userInvoice,
            string passwordInvoice,
            string email,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            Order = order;
            Description = description;
            Alias = alias;
            UrlLogo = urlLogo;
            CreatedOn = DateTime.UtcNow;
            UserName = userInvoice;
            Password = passwordInvoice;
            Email = email;
        }

        public static Result<Operation> Create(
            int order,
            string description,
            string alias,
            string urlLogo,
            Guid createBy,
            string userInvoice,
            string passwordInvoice,
            string email)
        {
            Guard.Against.NullOrWhiteSpace(description);
            Guard.Against.NullOrWhiteSpace(alias);
            Guard.Against.NullOrWhiteSpace(userInvoice);
            Guard.Against.NullOrWhiteSpace(passwordInvoice);
            Guard.Against.NullOrWhiteSpace(email);

            if (Errors.Any())
                return Result.Failure<Operation>(GetErrors());


            return Result.Success(new Operation(
                order,
                description,
                alias,
                urlLogo,
                userInvoice,
                passwordInvoice,
                email,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }


        public Result UpdateInformation(
            int order,
            string description,
            string alias,
            string urlLogo,
            string userInvoice,
            string passwordInvoice,
            string email,
            Guid userInSession)
        {
            Order = order;
            Description = description;
            Alias = alias;
            UrlLogo = urlLogo;
            UserName = userInvoice;
            Password = passwordInvoice;
            Email = email;

            Update(userInSession);

            return Result.Success();
        }

        public Result DeleteOperation(Guid userId)
        {
            Delete(userId);
            _restaurants.ForEach(x => x.DeleteRestaurant(userId));
            return Result.Success();
        }

        public Result RegisterNewRestaurant(int operationId,
            string description,
            string urlLogo,
            string email,
            Guid createBy)
        {
            Result<Restaurant> restaurant = Restaurant.Create(
                operationId,
                description,
                urlLogo,
                email,
                createBy);

            if (restaurant.IsFailure)
            {
                return Result.Failure<ResponseDefault>(restaurant.Error);
            }
            _restaurants.Add(restaurant.Value);
            return Result.Success();
        }

        public Result RegisterNewRoom(int operationId,
            string description,
            string title,
            string urlLogo,
            int guests,
            int roomType,
            string observation,
            Guid createBy)
        {
            Result<Room> room = Room.Create(
                operationId,
                description,
                title,
                urlLogo,
                guests,
                roomType,
                observation,
                createBy);

            if (room.IsFailure)
            {
                return Result.Failure<ResponseDefault>(room.Error);
            }
            _rooms.Add(room.Value);
            return Result.Success();
        }
    }
}