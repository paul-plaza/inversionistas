namespace Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationsAndRestaurantsNames
{
    public record RestaurantsNamesResponse
    {
        public int Id { get; init; }
        public int OperationId { get; set; }
        public string Name { get; init; }
        public string Image { get; set; }
    }
}