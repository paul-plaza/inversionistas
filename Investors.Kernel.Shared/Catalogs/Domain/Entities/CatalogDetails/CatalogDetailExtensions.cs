using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response;

namespace Investors.Kernel.Shared.Catalogs.Domain.Entities.CatalogDetails
{
    public static class CatalogDetailExtensions
    {
        /// <summary>
        /// Transforma una operacion basica.
        /// </summary>
        /// <param name="catalogDetail"></param>
        /// <returns></returns>
        public static CatalogDetailResponse ToCatalogDetailResponse(this CatalogDetail catalogDetail)
        {
            return new CatalogDetailResponse(
                catalogDetail.Id,
                catalogDetail.Description,
                catalogDetail.Value
                );
        }
    }
}