using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Hangfire.Server;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants
{
    public class Restaurant : BaseEntity<int>
    {
        /// <summary>
        /// Descripción de restaurante
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// url del logo de restaurante
        /// </summary>
        public string UrlLogo { get; private set; }

        /// <summary>
        /// Correo del encargado para enviar notificaciones
        /// </summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        public string Email { get; private set; }

        /// <summary>
        /// Relación con operación
        /// </summary>
        public virtual Operation Operation { get; private set; }

        /// <summary>
        /// Id de Operación
        /// </summary>
        public int OperationId { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<MenuType> _menuTypes = new();

        /// <summary>
        /// Tipo de menus que posee cada restaurante
        /// </summary>
        public virtual IReadOnlyList<MenuType> MenuTypes => _menuTypes;

        protected Restaurant()
        {

        }

        private Restaurant(
            int operationId,
            string description,
            string urlLogo,
            string email,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            OperationId = operationId;
            Description = description;
            UrlLogo = urlLogo;
            Email = email;
        }

        public static Result<Restaurant> Create(
            int operationId,
            string description,
            string urlLogo,
            string email,
            Guid createBy)
        {
            Guard.Against.NullOrWhiteSpace(description);
            Guard.Against.NullOrWhiteSpace(urlLogo);
            Guard.Against.NullOrWhiteSpace(email);

            if (Errors.Any())
                return Result.Failure<Restaurant>(GetErrors());

            return Result.Success(new Restaurant(
                operationId,
                description,
                urlLogo,
                email,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result UpdateInformation(
            int operationId,
            string description,
            string urlLogo,
            string email,
            Guid userInSession)
        {
            OperationId = operationId;
            Description = description;
            UrlLogo = urlLogo;
            Email = email;

            Update(userInSession);
            return Result.Success();
        }

        public Result DeleteRestaurant(Guid userId)
        {
            _menuTypes.ForEach(x => x.DeleteMenuType(userId));
            Delete(userId);
            return Result.Success();
        }

        public Result RegisterNewMenuType(
            string description,
            string urlLogo,
            Guid createBy)
        {
            Result<MenuType> menuType = MenuType.Create(
                Id,
                description,
                urlLogo,
                createBy);

            if (menuType.IsFailure)
            {
                return Result.Failure<ResponseDefault>(menuType.Error);
            }

            _menuTypes.Add(menuType.Value);
            return Result.Success();
        }

        public Result DeleteMenuType(
            int menuTypeId,
            Guid userInSession)
        {
            MenuType menus = _menuTypes.Where(x => x.Id == menuTypeId).First();
            Result menutype = menus.DeleteMenuType(
                userInSession
                );

            if (menutype.IsFailure)
            {
                return Result.Failure<ResponseDefault>(menutype.Error);
            }
            return Result.Success();
        }
    }
}