using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Operations.Configuration.OperationEntity
{
    internal class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder.ToTable(nameof(Operation)).HasKey(e => e.Id);

            builder.HasIndex(x => x.Description)
                   .IsUnique();

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Operation)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.Order)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Orden de la operacion");

            builder.Property(x => x.Description)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripcion o nombre de la operacion");

            builder.Property(x => x.Alias)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Alias de la operacion");

            builder.Property(x => x.UrlLogo)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Ruta del logo de la operacion");

            builder.Property(x => x.UserName)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment("Nombre de usuario de la operación para factura");

            builder.Property(x => x.Password)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment("Contraseña de usuario de la operación para factura");

            builder.Property(x => x.Email)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment("Correo del encargado de la operación");

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(9)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(10)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(11)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(12)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasMany(e => e.Restaurants)
                .WithOne(x => x.Operation)
                .HasForeignKey(e => e.OperationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_OPERATION_WITH_MANY_RESTAURANTS");

            builder.HasMany(e => e.Rooms)
                .WithOne(x => x.Operation)
                .HasForeignKey(e => e.OperationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_OPERATION_WITH_MANY_ROOMS");
        }
    }
}