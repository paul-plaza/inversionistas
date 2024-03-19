using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Entities.Profiles;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.UserEntity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User)).HasKey(e => e.Id);

            //agrego indice a columna identification con status active
            builder.HasIndex(e => new
                {
                    e.Identification,
                    e.Status
                }, "IDX_UNIQUE_VALUES_IDENTIFICATION")
                .IsUnique()
                .HasFilter("Status = 1");


            builder.Property(x => x.Id)
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "Guid"));

            builder.Property(x => x.Identification)
                .HasColumnOrder(1)
                .HasMaxLength(User.IdentificationMaxLength)
                .HasComment(UserMetaDataConstants.Identification)
                .IsRequired();

            builder
                .Property(x => x.DisplayName)
                .HasColumnOrder(2)
                .HasMaxLength(User.DisplayMaxLength)
                .HasComment(UserMetaDataConstants.DisplayName)
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasColumnOrder(3)
                .HasMaxLength(User.NameMaxLength)
                .HasComment(UserMetaDataConstants.DisplayName)
                .IsRequired();

            builder
                .Property(x => x.SurName)
                .HasColumnOrder(4)
                .HasMaxLength(User.SurNameMaxLength)
                .HasComment(UserMetaDataConstants.DisplayName)
                .IsRequired();

            builder.Property(x => x.UserType)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(UserMetaDataConstants.UserType)
                .HasConversion(p => p.Value, p => UserType.From(p));

            builder.Property(x => x.Identification)
                .HasColumnOrder(6)
                .HasMaxLength(User.IdentificationMaxLength)
                .HasComment(UserMetaDataConstants.Identification)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment(UserMetaDataConstants.Email)
                .HasConversion(p => p.Value, p => Email.Create(p).Value);

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

            builder.HasOne(e => e.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<Profile>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_USER_WITH_ONE_PROFILE");

            builder.HasMany(e => e.Receipts)
                .WithOne(x => x.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_USER_WITH_MANY_RECEIPTS");

            builder.HasMany(e => e.Movements)
                .WithOne(x => x.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_USER_WITH_MANY_MOVEMENTS");

            builder.HasMany(e => e.Notifications)
                .WithOne(x => x.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ONE_USER_WITH_MANY_NOTIFICATIONS");
        }
    }
}