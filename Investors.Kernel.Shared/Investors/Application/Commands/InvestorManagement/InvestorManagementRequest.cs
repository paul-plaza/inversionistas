namespace Investors.Kernel.Shared.Investors.Application.Commands.InvestorManagement
{
    public class InvestorManagementRequest
    {
        public int OperationId { get; set; }

        public int TotalActions { get; set; }
        public string Identification { get; set; }
        public string FullNames { get; set; }
    }
}