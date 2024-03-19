using Investors.Client.Users.Domain.Entities;
using Investors.Client.Users.Domain.Entities.Notifications;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.NotificationEntity
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(Notification)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Notification)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.UserId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment(NotificationMetaData.UserId);

            builder.Property(x => x.Title)
                .HasMaxLength(60)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment(NotificationMetaData.Title);

            builder.Property(x => x.SubTitle)
                .HasMaxLength(120)
                .HasColumnOrder(3)
                .IsRequired(false)
                .HasComment(NotificationMetaData.SubTitle);

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment(NotificationMetaData.Description);

            builder.Property(x => x.NotificationType)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(NotificationMetaData.NotificationType)
                .HasConversion(p => p.Value, p => NotificationType.From(p));

            builder.Property(x => x.OperationId)
                .HasColumnOrder(6)
                .IsRequired(false)
                .HasComment(NotificationMetaData.OperationId);

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
        }
    }
}