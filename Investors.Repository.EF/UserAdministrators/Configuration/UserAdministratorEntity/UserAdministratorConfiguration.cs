using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.UserAdministrators.Configuration.UserAdministratorEntity
{
    public class UserAdministratorConfiguration : IEntityTypeConfiguration<UserAdministrator>
    {

        public void Configure(EntityTypeBuilder<UserAdministrator> builder)
        {
            builder.ToTable(nameof(UserAdministrator)).HasKey(e => e.Id);

            builder.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_UNIQUE_EMAIL");

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(UserAdministrator)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "Int"));

            builder.Property(p => p.Email)
                .HasColumnOrder(1)
                .IsRequired()
                .HasMaxLength(UserAdministrator.EmailMaxLength)
                .HasComment(UserAdministratorMetaData.Email)
                .HasConversion(p => p.Value, p => Email.Create(p).Value);

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(4)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(5)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasMany(e => e.Options)
                .WithOne(x => x.UserAdministrator)
                .HasForeignKey(e => e.UserAdministratorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_USER_ADMINISTRATOR_WITH_MANY_MENUS");

            builder.HasMany(e => e.Operations)
                .WithOne(x => x.UserAdministrator)
                .HasForeignKey(e => e.UserAdministratorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_USER_ADMINISTRATOR_WITH_MANY_OPERATIONS");
        }
    }
}