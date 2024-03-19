namespace Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationsAndRestaurantsNames
{
    public record OperationsNamesResponse
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public List<RestaurantsNamesResponse> Restaurants { get; init; }
    }


}