using Investors.Kernel.Shared.Catalogs.Domain.Entities.CatalogDetails;
using Investors.Kernel.Shared.Catalogs.Domain.Entities.Categories;
using Investors.Shared.Domain;

namespace Investors.Kernel.Shared.Catalogs.Domain.Aggregate
{
    public class Catalog : BaseEntity<int>
    {
        /// <summary>
        /// Descripción
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<CatalogDetail> _catalogDetails = new();

        /// <summary>
        /// Detalles que poseé el catálogo
        /// </summary>
        public virtual IReadOnlyList<CatalogDetail> CatalogDetails => _catalogDetails;

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<Category> _categories = new();

        /// <summary>
        /// Detalles que poseé el catálogo
        /// </summary>
        public virtual IReadOnlyList<Category> Categories => _categories;
    }
}