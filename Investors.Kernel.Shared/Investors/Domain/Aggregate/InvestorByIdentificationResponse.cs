namespace Investors.Kernel.Shared.Investors.Domain.Aggregate
{
    public record InvestorByIdentificationResponse(
        string Identification,
        Guid Id,
        List<OperationsResponse> Operations
        );

    public record OperationsResponse(
        int OperationId
        );
}