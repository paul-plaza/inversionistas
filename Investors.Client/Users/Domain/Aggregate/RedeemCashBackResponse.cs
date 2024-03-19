namespace Investors.Client.Users.Domain.Aggregate
{
    public record RedeemCashBackResponse(
        Guid TransactionId,
        string Message,
        string Qr
        );
}