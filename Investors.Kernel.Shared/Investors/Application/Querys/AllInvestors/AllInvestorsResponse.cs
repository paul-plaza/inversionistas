namespace Investors.Kernel.Shared.Investors.Application.Querys.AllInvestors
{
    public record AllInvestorsResponse
    {
        public string Identification { get; init; }

        public string Names { get; init; }

        public IEnumerable<OperationsResponse> Operations { get; init; }

    }

    public record OperationsResponse
    {
        public int Id { get; set; }
        public string Description { get; init; }

        public int TotalActions { get; init; }
        public string UlrLogo { get; set; }
    }
}