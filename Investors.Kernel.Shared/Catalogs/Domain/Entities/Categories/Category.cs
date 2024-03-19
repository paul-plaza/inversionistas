using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Domain;

namespace Investors.Kernel.Shared.Catalogs.Domain.Entities.Categories
{
    public class Category : BaseEntity<int>
    {
        /// <summary>
        /// Descripción
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Valor
        /// </summary>
        public int Points { get; private set; }

        /// <summary>
        /// Valor de puntos para subir de nivel
        /// </summary>
        public int NextLevelPoints { get; private set; }

        /// <summary>
        /// Noches
        /// </summary>
        public int Nights { get; private set; }

        /// <summary>
        /// Valor de noches para subir de nivel
        /// </summary>
        public int NextLevelNights { get; private set; }

        /// <summary>
        /// Observación
        /// </summary>
        public string Observation { get; private set; }

        /// <summary>
        /// Porcentaje
        /// </summary>
        public int Percent { get; private set; }

        /// <summary>
        /// Relación con catálogo
        /// </summary>
        public virtual Catalog Catalog { get; private set; }

        /// <summary>
        /// Id de catálogo
        /// </summary>
        public int CatalogId { get; private set; }

        /// <summary>
        /// Imagen de beneficios por categoria
        /// </summary>
        public string UrlImage { get; private set; }
    }
}