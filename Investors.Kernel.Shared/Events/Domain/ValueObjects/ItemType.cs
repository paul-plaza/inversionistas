using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Domain.ValueObjects
{
    public class ItemType : IValueObject
    {
        public enum EventItemType
        {
            Grid = 1,
            List = 2,
            ImageList = 3
        }

        public static readonly ItemType Grid = new(EventItemType.Grid);
        public static readonly ItemType List = new(EventItemType.List);
        public static readonly ItemType ImageList = new(EventItemType.ImageList);

        public static readonly ItemType[] AllItemType =
        {
            Grid, List, ImageList
        };

        protected ItemType()
        {

        }
        protected ItemType(EventItemType value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static ItemType From(int i)
        {
            return AllItemType.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)EventItemType.Grid:
                    return "grid";
                case (int)EventItemType.List:
                    return "list";
                case (int)EventItemType.ImageList:                    
                    return "imagelist";
                default:
                    throw new ArgumentException("Tipo no implementado"); //TODO: agregar exception personalizada
            }
        }
    }
}