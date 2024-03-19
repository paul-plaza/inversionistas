using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Domain.Entities.Menus
{
    public class Menu : BaseEntity<int>
    {
        /// <summary>
        /// Descripción de menú
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Titulo del menú
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// url del logo de menú
        /// </summary>
        public int Points { get; private set; }

        /// <summary>
        /// Relación con Tipo menú
        /// </summary>
        public virtual MenuType MenuType { get; private set; }

        /// <summary>
        /// Id de Tipo menú
        /// </summary>
        public int MenuTypeId { get; private set; }

        protected Menu()
        {

        }

        private Menu(
        string description,
        string title,
        int points,
        Guid createBy,
        DateTime createOn,
        Status status
        ) : base(createBy, createOn, status)
        {
            Description = description;
            Title = title;
            Points = points;
        }

        public static Result<Menu> Create(
        string description,
        string title,
        int points,
        Guid createBy)
        {
            Guard.Against.NullOrWhiteSpace(description);
            Guard.Against.NullOrWhiteSpace(title);

            if (Errors.Any())
                return Result.Failure<Menu>(GetErrors());

            return Result.Success(new Menu(
                description,
                title,
                points,
                createBy,
                DateTime.UtcNow,
                Status.Active));
        }

        public Result DeleteMenu(Guid userId)
        {
            Delete(userId);
            return Result.Success();
        }

        public Result UpdateInformation(
        string title,
        string description,        
        int points,
        Guid userInSession)
        {
            Title= title;
            Description = description;
            Points= points;
            Update(userInSession);
            return Result.Success();
        }
    }
}