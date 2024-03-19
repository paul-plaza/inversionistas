using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes
{
    public class MenuType : BaseEntity<int>
    {
        /// <summary>
        /// Descripción de tipo de menú
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// url del logo de tipo de menú
        /// </summary>
        public string UrlLogo { get; private set; }

        /// <summary>
        /// Relación con restaurante
        /// </summary>
        public virtual Restaurant Restaurant { get; private set; }

        /// <summary>
        /// Id de restaurante
        /// </summary>
        public int RestaurantId { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<Menu> _menus = new();

        /// <summary>
        /// Menus que posee cada tipo de menú
        /// </summary>
        public virtual IReadOnlyList<Menu> Menus => _menus;

        protected MenuType()
        {

        }

        private MenuType(int restaurantId,
            string description,
            string urlLogo,
            Guid createBy,
            DateTime createOn,
            Status status
            ) : base(createBy, createOn, status)
        {
            RestaurantId = restaurantId;
            Description = description;
            UrlLogo = urlLogo;
        }

        public static Result<MenuType> Create(
            int restaurantId,
            string description,
            string urlLogo,
            Guid createBy)
        {
            Guard.Against.NullOrWhiteSpace(description);

            if (Errors.Any())
                return Result.Failure<MenuType>(GetErrors());

            return Result.Success(new MenuType(
                restaurantId,
                description,
                urlLogo,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result UpdateInformation(
            string description,
            string urlLogo,
            Guid userInSession)
        {
            Description = description;
            UrlLogo = urlLogo;

            Update(userInSession);
            return Result.Success();
        }

        public Result RegisterNewMenus(List<Menu> menus)
        {
            _menus.AddRange(menus);
            return Result.Success();
        }

        public Result DeleteMenuType(Guid userId)
        {
            _menus.ForEach(x => x.DeleteMenu(userId));
            Delete(userId);
            return Result.Success();
        }

        public Result DeleteAllMenu(Guid userId)
        {
            _menus.ForEach(x => x.DeleteMenu(userId));
            return Result.Success();
        }

        public Result DeleteMenu(int menuId, Guid userId)
        {
            _menus.First(x => x.Id == menuId).DeleteMenu(userId);
            return Result.Success();
        }

    }
}