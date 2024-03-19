namespace Investors.Administrator.Reports.Domain
{
    public record ViewMovementsOfCashback(
        Guid Id,
        string Operation,
        DateTime Date,
        string Investor,
        string Identification,
        int TotalQuantity,
        string Menu,
        int Points,
        string Operations
        );
}