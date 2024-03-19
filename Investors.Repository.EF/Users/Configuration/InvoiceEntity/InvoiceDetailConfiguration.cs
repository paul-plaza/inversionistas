using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.InvoiceEntity
{
    internal class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable(nameof(InvoiceDetail)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(InvoiceDetail)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.InvoiceId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment(InvoiceDetailMetaData.InvoiceId);

            builder.Property(x => x.Group)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment(InvoiceDetailMetaData.Group);

            builder.Property(x => x.GroupDetail)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment(InvoiceDetailMetaData.GroupDetail);

            builder.Property(x => x.TotalValue)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment(InvoiceDetailMetaData.TotalValue);

            builder.Property(x => x.CheckIn)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasComment(InvoiceDetailMetaData.CheckIn);

            builder.Property(x => x.CheckOut)
                .HasColumnOrder(6)
                .IsRequired(false)
                .HasComment(InvoiceDetailMetaData.CheckOut);

            builder.Property(x => x.Nights)
                .HasColumnOrder(7)
                .IsRequired(false)
                .HasComment(InvoiceDetailMetaData.Nights);

            builder.Property(x => x.TransactionType)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment(InvoiceDetailMetaData.TransactionType);

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(9)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(10)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(11)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(12)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(13)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));
        }
    }
}