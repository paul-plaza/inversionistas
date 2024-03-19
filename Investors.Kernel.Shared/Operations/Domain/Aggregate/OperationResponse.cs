namespace Investors.Kernel.Shared.Operations.Domain.Aggregate
{
    public record OperationResponse(
        int Id,
        string Description,
        string Alias,
        string UrlLogo,
        int Order,
        string UserName,
        string Password,
        string Email);
}