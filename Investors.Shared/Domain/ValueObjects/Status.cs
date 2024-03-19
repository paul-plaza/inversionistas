using System;
namespace Investors.Shared.Domain.ValueObjects
{
    public record Status : IValueObject
    {
        public enum StatusType
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }

        public static readonly Status Active = new(StatusType.Active);
        public static readonly Status Inactive = new(StatusType.Inactive);
        public static readonly Status Deleted = new(StatusType.Deleted);

        public static readonly Status[] AllStates =
        {
            Active, Inactive, Deleted
        };

        protected Status()
        {

        }
        protected Status(StatusType value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static Status From(int i)
        {
            return AllStates.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)StatusType.Active:
                    return "Activo";
                case (int)StatusType.Inactive:
                    return "Inactivo";
                case (int)StatusType.Deleted:
                    return "Eliminado";
                default:
                    return "Sin Estado";
            }
        }
    }
}