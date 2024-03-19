using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.ReceiptEntity
{
    internal class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.ToTable(nameof(Receipt)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Receipt)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.UserId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id del usuario que registra el recibo");

            builder.Property(x => x.Date)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Fecha de la factura");

            builder.Property(x => x.Identification)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Identificacion de referido");

            builder.Property(x => x.IsSync)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Es sincronizada");

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

            builder.HasMany(e => e.Invoices)
                .WithOne(x => x.Receipt)
                .HasForeignKey(e => e.ReceiptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_RECEIPT_WITH_MANY_INVOICES");
        }
    }
}
