using Investors.Kernel.Shared.Catalogs.Domain.Entities.CatalogDetails;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Catalogs.Configuration.CatalogDetailEntity
{
    internal class CatalogDetailConfiguration : IEntityTypeConfiguration<CatalogDetail>
    {
        public void Configure(EntityTypeBuilder<CatalogDetail> builder)
        {
            builder.ToTable(nameof(CatalogDetail)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(CatalogDetail)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.CatalogId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de Catálogo a la que pertenece este detalle");

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripcion o nombre del detalle catálogo");

            builder.Property(x => x.Value)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Valor de detalle catálogo");

            builder.Property(x => x.Observation)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasComment("Observación de detalle catálogo");

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(7)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(8)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(9)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));
        }
    }
}