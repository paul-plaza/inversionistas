using Investors.Administrator.Reports.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Views.Movements.ViewMovementsOfNights
{
    public class ViewMovementsOfNightConfiguration : IEntityTypeConfiguration<ViewMovementsOfNight>
    {

        public void Configure(EntityTypeBuilder<ViewMovementsOfNight> builder)
        {
            builder.HasNoKey();

            builder.ToView("ViewMovementsOfNights");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Operation)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Investor)
                .IsRequired();

            builder.Property(e => e.Identification)
                .IsRequired();

            builder.Property(e => e.Room)
                .IsRequired();

            builder.Property(e => e.StartDate)
                .IsRequired();

            builder.Property(e => e.EndDate)
                .IsRequired();

            builder.Property(e => e.Operations)
                .IsRequired();
        }
    }
}