using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investors.Repository.EF.Users.Configuration.Transactions
{
    internal class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.ToTable(nameof(Movement)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "Guid"));

            builder.Property(x => x.OperationId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de operación");

            builder.Property(x => x.RestaurantId)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasComment(MovementMetaDataConstants.RestaurantId);

            builder.Property(x => x.RoomId)
                .HasColumnOrder(3)
                .IsRequired(false)
                .HasComment(MovementMetaDataConstants.RoomId);

            builder.Property(x => x.TransactionType)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Tipo de transacción")
                .HasConversion(p => p.Value, p => TransactionType.From(p));

            builder.Property(x => x.TransactionState)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment("Estado de transacción o movimiento")
                .HasConversion(p => p.Value, p => TransactionState.From(p));

            builder.Property(x => x.TotalToRedeem)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment("Total del valor acumulado en noches o puntos");

            builder.Property(x => x.TotalItems)
                .HasColumnOrder(7)
                .IsRequired(false)
                .HasComment(MovementMetaDataConstants.TotalItems);

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

            builder.HasMany(e => e.CashbackDetails)
                .WithOne(x => x.Movement)
                .HasForeignKey(e => e.MovementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_MOVEMENT_WITH_MANY_CASHBACK_DETAILS");

            builder.HasOne(e => e.NightsDetail)
                .WithOne(x => x.Movement)
                .HasForeignKey<NightsDetail>(e => e.MovementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_MOVEMENT_WITH_ONE_NIGHTS_DETAILS");
        }
    }
}