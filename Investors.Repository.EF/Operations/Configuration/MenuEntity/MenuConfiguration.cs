using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Operations.Configuration.MenuEntity
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable(nameof(Menu)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Menu)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.MenuTypeId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de tipo de menu al que pertenece");

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripcion o nombre del menú");

            builder.Property(x => x.Title)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Título del menú");

            builder.Property(x => x.Points)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Valor en puntos del articulo");

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