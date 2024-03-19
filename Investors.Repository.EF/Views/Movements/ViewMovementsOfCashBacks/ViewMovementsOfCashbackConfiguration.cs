using Investors.Administrator.Reports.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.Views.Movements.ViewMovementsOfCashBacks
{
    public class ViewMovementsOfCashbackConfiguration : IEntityTypeConfiguration<ViewMovementsOfCashback>
    {

        public void Configure(EntityTypeBuilder<ViewMovementsOfCashback> builder)
        {
            builder.HasNoKey();

            builder.ToView("ViewMovementsOfCashback");

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

            builder.Property(e => e.TotalQuantity)
                .IsRequired();

            builder.Property(e => e.Menu)
                .IsRequired();

            builder.Property(e => e.Points)
                .IsRequired();

            builder.Property(e => e.Operations)
                .IsRequired();
        }
    }
}