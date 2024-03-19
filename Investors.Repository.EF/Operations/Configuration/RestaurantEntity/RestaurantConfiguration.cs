using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Operations.Configuration.RestaurantEntity
{
    internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable(nameof(Restaurant)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Restaurant)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.OperationId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de Operacion a la que pertenece");

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripcion o nombre del restaurante");

            builder.Property(x => x.UrlLogo)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Ruta del logo del restaurante");

            builder.Property(x => x.Email)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Correo del encargado del restaurante");

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

            builder.HasMany(e => e.MenuTypes)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_RESTAURANT_WITH_MANY_MENUTYPES");
        }
    }
}