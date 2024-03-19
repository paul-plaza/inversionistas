namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response
{
    public record CatalogResponse(
        int Id,
        string Description,
        IReadOnlyCollection<CatalogDetailResponse> CatalogDetails
        );
}