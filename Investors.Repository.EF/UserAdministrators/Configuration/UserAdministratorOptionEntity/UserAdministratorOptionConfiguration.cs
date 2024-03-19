using Investors.Administrator.Users.Domain.Entities;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorOptionEntity
{
    public class UserAdministratorOptionConfiguration : IEntityTypeConfiguration<UserAdministratorOption>
    {

        public void Configure(EntityTypeBuilder<UserAdministratorOption> builder)
        {
            builder.ToTable(nameof(UserAdministratorOption)).HasKey(e => e.Id);

            builder.HasIndex(x => new
            {
                x.UserAdministratorId,
                x.OptionId
            }).IsUnique();

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(UserAdministratorOption)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "Int"));

            builder.Property(x => x.UserAdministratorId)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment(UserAdministratorOptionMetaData.UserAdministratorId);

            builder.Property(x => x.OptionId)
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