using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Events.Configuration.EventDetailEntity
{
    internal class EventDetailConfiguration : IEntityTypeConfiguration<EventDetail>
    {
        public void Configure(EntityTypeBuilder<EventDetail> builder)
        {
            builder.ToTable(nameof(EventDetail)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(EventDetail)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.EventId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de Evento a la que pertenece este detalle");

            builder.Property(x => x.Title)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Titulo del item");

            builder.Property(x => x.Description)
                .HasColumnOrder(3)
                .IsRequired(false)
                .HasComment("Descripcion o nombre del menú");

            builder.Property(x => x.UrlLogo)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasComment("Ruta del logo del detalle beneficio");

            builder.Property(x => x.UrlToOpen)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasComment("link de imagen a mostrar");

            builder.Property(x => x.OperationId)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment("operación a la que pertenece el evento");

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(9)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(10)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(11)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasMany(e => e.EventSubDetails)
                .WithOne(x => x.EventDetail)
                .HasForeignKey(e => e.EventDetailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_EVENT_DETAIL_WITH_MANY_EVENT_SUB_DETAILS");
        }
    }
}