using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Investors.Domain.Entities;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Investors.Configuration
{
    internal class InvestorOperationsConfiguration : IEntityTypeConfiguration<InvestorOperation>
    {
        public void Configure(EntityTypeBuilder<InvestorOperation> builder)
        {
            builder.ToTable(nameof(InvestorOperation)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment("Id del registro");

            builder.Property(x => x.OperationId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Identificacion del cliente");

            builder.Property(x => x.InvestorId)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Identificacion del cliente");

            builder.Property(x => x.TotalActions)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment("Total de acciones en operacion");

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

            builder.HasOne(e => e.Operation)
                .WithMany(x => x.InvestorOperations)
                .HasForeignKey(e => e.OperationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_OPERATION_WITH_MANY_INVESTOR");

            builder.HasOne(e => e.Investor)
                .WithMany(x => x.InvestorOperations)
                .HasForeignKey(e => e.InvestorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_INVESTOR_WITH_MANY_OPERATION");
        }
    }
}