using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.Transactions
{
    internal class NightDetailConfiguration : IEntityTypeConfiguration<NightsDetail>
    {
        public void Configure(EntityTypeBuilder<NightsDetail> builder)
        {
            builder.ToTable(nameof(NightsDetail)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(NightsDetail)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.MovementId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de movimiento");

            builder.Property(x => x.Observation)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasComment("Observación");

            builder.Property(x => x.DateStart)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Fecha ingreso");

            builder.Property(x => x.DateEnd)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Fecha salida");

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