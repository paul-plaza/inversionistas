namespace Investors.Client.Users.Application.Commands.Requests
{
    public record ReedemCashBackRequest(int RestaurantId, int OperationId, IEnumerable<int> MenuIds);
}