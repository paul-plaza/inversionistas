namespace Investors.Client.Users.Application.Commands.Requests
{
    public record RegisterRedeemNightsRequest(int OperationId, int RoomId, string? Observation, DateTime DateStart, DateTime DateEnd);
}