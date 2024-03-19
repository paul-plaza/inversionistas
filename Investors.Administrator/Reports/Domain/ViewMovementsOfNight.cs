namespace Investors.Administrator.Reports.Domain
{
    public record ViewMovementsOfNight(
        Guid Id,
        string Operation,
        DateTime Date,
        string Investor,
        string Identification,
        string Room,
        DateTime StartDate,
        DateTime EndDate,
        string Operations
        );
}