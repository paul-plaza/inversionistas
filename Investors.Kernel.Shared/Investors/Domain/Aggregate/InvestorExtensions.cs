namespace Investors.Kernel.Shared.Investors.Domain.Aggregate
{
    public static class InvestorExtensions
    {
        public static InvestorByIdentificationResponse ToInvestorByIdentificationResponse(this Investor investor)
        {
            return new InvestorByIdentificationResponse(
                Identification: investor.Identification,
                Id: investor.Id,
                Operations: investor.InvestorOperations
                    .Select(x => new OperationsResponse(x.OperationId))
                    .ToList()
                );
        }
    }
}