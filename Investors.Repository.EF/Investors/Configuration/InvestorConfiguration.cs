using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Investors.Configuration
{
    internal class InvestorConfiguration : IEntityTypeConfiguration<Investor>
    {
        public void Configure(EntityTypeBuilder<Investor> builder)
        {
            builder.ToTable(nameof(Investor)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment("Id del registro");

            builder.Property(x => x.FullNames)
                .HasColumnOrder(1)
                .HasMaxLength(Investor.FullNamesMaxLength)
                .IsRequired()
                .HasComment("Nombre del cliente");

            builder.Property(x => x.Identification)
                .HasColumnOrder(2)
                .HasMaxLength(Investor.IdentificationMaxLength)
                .IsRequired()
                .HasComment("Identificacion del cliente");

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(6)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));
        }
    }
}