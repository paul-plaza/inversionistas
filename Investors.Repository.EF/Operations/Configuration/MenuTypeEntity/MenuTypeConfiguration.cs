using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Operations.Configuration.MenuTypeEntity
{
    internal class MenuTypeConfiguration : IEntityTypeConfiguration<MenuType>
    {
        public void Configure(EntityTypeBuilder<MenuType> builder)
        {
            builder.ToTable(nameof(MenuType)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(MenuType)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.RestaurantId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de Restaurante al que pertenece");

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripcion o nombre del restaurante");

            builder.Property(x => x.UrlLogo)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Ruta del logo del restaurante");

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(6)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(7)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasMany(e => e.Menus)
                .WithOne(x => x.MenuType)
                .HasForeignKey(e => e.MenuTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_TYPEMENU_WITH_MANY_MENUS");
        }
    }
}