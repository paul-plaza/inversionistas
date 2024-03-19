using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Operations.Configuration.RoomEntity
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable(nameof(Room)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Room)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.OperationId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment("Id de Operacion a la que pertenece");

            builder.Property(x => x.Description)
                .HasMaxLength(Room.DescriptionMaxLength)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment("Descripción de habitación");

            builder.Property(x => x.Title)
                .HasColumnOrder(3)
                .IsRequired()
                .HasMaxLength(Room.TitleMaxLength)
                .HasComment("Título de habitación");

            builder.Property(x => x.UrlLogo)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment("Ruta del logo de habitación");

            builder.Property(x => x.Observation)
                .HasMaxLength(Room.ObservationMaxLength)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment("Observación de la habitación");

            builder.Property(x => x.Guests)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment("Número máximo de huéspedes por habitación");

            builder.Property(p => p.RoomTypeId)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment("Tipo de habitación");

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
        }

    }

}