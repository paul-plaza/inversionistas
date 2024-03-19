using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.InvoiceEntity
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable(nameof(Invoice)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Invoice)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.ReceiptId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id del recibo");

            builder.Property(x => x.Number)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Numero de la factura");

            builder.Property(x => x.Date)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Fecha de la factura");

            builder.Property(x => x.Identification)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment(InvoiceMetaData.Identification);

            builder.Property(x => x.IdentificationInvestor)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(InvoiceMetaData.IdentificationInvestor);

            builder.Property(x => x.TotalInvoice)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment(InvoiceMetaData.TotalInvoice);

            builder.Property(x => x.OperationId)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment(InvoiceMetaData.OperationId);

            builder.Property(x => x.InvoiceType)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment(InvoiceMetaData.InvoiceType)
                .HasConversion(p => p.Value, p => InvoiceType.From(p));

            builder.Property(x => x.TotalPoints)
                .HasColumnOrder(9)
                .IsRequired(false)
                .HasComment(InvoiceMetaData.TotalPoints);

            builder.Property(x => x.TotalNights)
                .HasColumnOrder(10)
                .IsRequired(false)
                .HasComment(InvoiceMetaData.TotalNights);

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(11)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(12)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(13)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(14)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(15)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasMany(e => e.InvoiceDetails)
                .WithOne(x => x.Invoice)
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_INVOICE_WITH_MANY_INVOICE_DETAILS");
        }
    }
}