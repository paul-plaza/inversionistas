using Investors.Kernel.Shared.Events.Domain.Entities.EventSubDetails;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Events.Configuration.EventSubDetailEntity
{
    internal class EventSubDetailConfiguration : IEntityTypeConfiguration<EventSubDetail>
    {
        public void Configure(EntityTypeBuilder<EventSubDetail> builder)
        {
            builder.ToTable(nameof(EventSubDetail)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(EventSubDetail)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.EventDetailId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de evento detalle a la que pertenece este sub detalle");

            builder.Property(x => x.Title)
                .HasColumnOrder(2)
                .IsRequired(false)
                .HasComment("Título o nombre del detalle beneficio");

            builder.Property(x => x.Description)
                .HasColumnOrder(3)
                .IsRequired(false)
                .HasComment("Descripción de detalle beneficio");

            builder.Property(x => x.Image)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasComment("Imagen");

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