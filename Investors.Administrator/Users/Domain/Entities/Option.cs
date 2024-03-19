using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.ValueObjects;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Users.Domain.Entities
{
    public class Option : BaseEntity<int>
    {
        public const int NameMaxLength = 50;

        public const int DescriptionMaxLength = 100;

        public const int RouteMaxLength = 150;

        public const int IconMaxLength = 50;

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Route { get; private set; }

        public Icon Icon { get; private set; }

        public Order Order { get; private set; }

        private Option(
            string name,
            string description,
            string route,
            Icon icon,
            Order order,
            Guid createdBy,
            DateTime createdOn,
            Status status) : base(createdBy, createdOn, status)
        {
            Name = name;
            Description = description;
            Route = route;
            Icon = icon;
            Order = order;
        }

        private Option(
            int id,
            string name,
            string description,
            string route,
            Icon icon,
            Order order,
            Guid createdBy,
            DateTime createdOn,
            Status status) : base(createdBy, createdOn, status)
        {
            Id = id;
            Name = name;
            Description = description;
            Route = route;
            Icon = icon;
            Order = order;
        }

        protected Option()
        {

        }

        public static Result<Option> Create(
            string name,
            string description,
            string route,
            Icon icon,
            Order order,
            Guid createdBy,
            DateTime createdOn,
            Status status)
        {
            return Result.Success(new Option(name, description, route, icon, order, createdBy, createdOn, status));
        }

        public static Result<Option> CreateSeed(
            int id,
            string name,
            string description,
            string route,
            Icon icon,
            Order order,
            Guid createdBy,
            DateTime createdOn,
            Status status)
        {
            return Result.Success(new Option(id, name, description, route, icon, order, createdBy, createdOn, status));
        }
    }
}