using Investors.Kernel.Shared.Catalogs.Domain.Entities.Categories;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Catalogs.Configuration.CategoryEntity
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Category)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.CatalogId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de Catálogo a la que pertenece esta categoría");

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripcion o nombre del detalle catálogo");

            builder.Property(x => x.Points)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Puntos para redimir en restaurants");

            builder.Property(x => x.NextLevelPoints)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Puntos requeridos para subir de categoría");

            builder.Property(x => x.Nights)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment("Noches para redimir hospedajes");

            builder.Property(x => x.NextLevelNights)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment("Noches requeridas para subir de categoría");

            builder.Property(x => x.Observation)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment("Observación de categoría");

            builder.Property(x => x.Percent)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment("Porcentaje para alcanzar siguiente categoría");

            builder.Property(x => x.UrlImage)
                .HasColumnOrder(9)
                .IsRequired()
                .HasComment(CategoryMetaDataConstants.UrlImage);

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(10)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(11)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(12)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(13)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(14)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));
        }
    }
}