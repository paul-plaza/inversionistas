using Investors.Client.Users.Domain.Entities.Profiles;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Users.Configuration.ProfileEntity
{
    internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable(nameof(Profile)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Profile)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "int"));

            builder.Property(x => x.CashBackToRedeem)
                .HasColumnOrder(1)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.CashBackToRedeem);

            builder.Property(x => x.AccumulativeNights)
                .HasColumnOrder(2)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.AccumulativeNights);

            builder.Property(x => x.NightsToRedeem)
                .HasColumnOrder(3)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.NightsToRedeem);

            builder.Property(x => x.HistoryCashBack)
                .HasColumnOrder(4)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.HistoryCashBack);

            builder.Property(x => x.HistoryNights)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.HistoryNights);

            builder.Property(p => p.Category)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.Category)
                .HasConversion(p => p.Value, p => UserCategory.From(p));

            builder.Property(x => x.TotalAccumulatedInvoice)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalAccumulatedInvoice);

            builder.Property(x => x.TotalMonthlyCashBackToRedeem)
                .HasColumnOrder(8)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalMonthlyCashBackToRedeem);

            builder.Property(x => x.TotalMonthlyCashBackClaimed)
                .HasColumnOrder(9)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalMonthlyCashBackClaimed);

            builder.Property(x => x.TotalCashBackClaimed)
                .HasColumnOrder(10)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalCashBackClaimed);

            builder.Property(x => x.TotalMonthlyNightsClaimed)
                .HasColumnOrder(11)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalMonthlyNightsClaimed);

            builder.Property(x => x.TotalMonthlyNightsToRedeem)
                .HasColumnOrder(12)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalMonthlyNightsToRedeem);

            builder.Property(x => x.TotalNightsClaimed)
                .HasColumnOrder(13)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalNightsClaimed);

            builder.Property(x => x.FirstInvoiceRegistrationDate)
                .HasColumnOrder(14)
                .IsRequired(false)
                .HasComment(ProfileMetaDataConstants.FirstInvoiceRegistrationDate);

            builder.Property(x => x.TotalMoneyAccumulated)
                .HasColumnOrder(15)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalMoneyAccumulated);

            builder.Property(x => x.TotalNightsAccumulated)
                .HasColumnOrder(16)
                .IsRequired()
                .HasComment(ProfileMetaDataConstants.TotalNightsAccumulated);

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(17)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(18)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(19)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(20)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(21)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));
        }
    }
}