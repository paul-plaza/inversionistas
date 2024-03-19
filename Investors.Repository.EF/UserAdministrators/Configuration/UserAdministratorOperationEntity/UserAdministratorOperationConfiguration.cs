using Investors.Administrator.Users.Domain.Entities;
using Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorOptionEntity;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorOperationEntity
{
    public class UserAdministratorOperationConfiguration : IEntityTypeConfiguration<UserAdministratorOperation>
    {

        public void Configure(EntityTypeBuilder<UserAdministratorOperation> builder)
        {
            builder.ToTable(nameof(UserAdministratorOperation)).HasKey(e => e.Id);

            builder.HasIndex(x => new
            {
                x.UserAdministratorId,
                x.OperationId
            }).IsUnique();

            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(UserAdministratorOperation)))
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "Int"));

            builder.Property(x => x.UserAdministratorId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment(UserAdministratorOptionMetaData.UserAdministratorId);

            builder.Property(x => x.OperationId)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment(UserAdministratorOptionMetaData.OptionId);

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