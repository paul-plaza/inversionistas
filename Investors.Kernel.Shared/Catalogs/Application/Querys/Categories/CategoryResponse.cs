namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Categories
{
    public record CategoryResponse(
        int Id,
        string Description,
        int Points,
        int Nights,
        int NextLevelPoints,
        int NextLevelNights,
        string Observation,
        int Percent,
        string Image
        );
}