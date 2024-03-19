using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Catalogs.Configuration.CatalogEntity
{
    internal class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.ToTable(nameof(Catalog)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Catalog)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.Description)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Descripcion o nombre del menú");

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasMany(e => e.CatalogDetails)
                .WithOne(x => x.Catalog)
                .HasForeignKey(e => e.CatalogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_CATALOG_WITH_MANY_DETAILS");

            builder.HasMany(e => e.Categories)
                .WithOne(x => x.Catalog)
                .HasForeignKey(e => e.CatalogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_CATALOG_WITH_MANY_CATEGORIES");
        }
    }
}