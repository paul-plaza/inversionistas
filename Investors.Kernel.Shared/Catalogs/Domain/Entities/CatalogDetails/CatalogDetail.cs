using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Domain;

namespace Investors.Kernel.Shared.Catalogs.Domain.Entities.CatalogDetails
{
    public class CatalogDetail : BaseEntity<int>
    {
        /// <summary>
        /// Descripción
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Valor
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Observación
        /// </summary>
        public string? Observation { get; private set; }

        /// <summary>
        /// Relación con catálogo
        /// </summary>
        public virtual Catalog Catalog { get; private set; }

        /// <summary>
        /// Id de catálogo
        /// </summary>
        public int CatalogId { get; private set; }

        protected CatalogDetail()
        {

        }

        private CatalogDetail(string description)
        {

        }
    }
}