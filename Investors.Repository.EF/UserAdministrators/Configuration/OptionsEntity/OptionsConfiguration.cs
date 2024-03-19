using Investors.Administrator.Users.Domain.Entities;
using Investors.Administrator.Users.Domain.ValueObjects;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;
using Investors.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Investors.Repository.EF.UserAdministrators.Configuration.OptionsEntity
{
    public class OptionsConfiguration : IEntityTypeConfiguration<Option>
    {

        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable(nameof(Option)).HasKey(e => e.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql(string.Format(AuditMetaDataConstants.Sequence, nameof(Option)))
                .HasColumnOrder(0)
                .IsRequired()
                .HasComment(string.Format(AuditMetaDataConstants.Id, "Int"));

            builder.Property(e => e.Name)
                .HasColumnOrder(1)
                .HasMaxLength(Option.NameMaxLength)
                .IsRequired()
                .HasComment(OptionsMetaData.Name);

            builder.Property(e => e.Description)
                .HasColumnOrder(2)
                .HasMaxLength(Option.DescriptionMaxLength)
                .IsRequired()
                .HasComment(OptionsMetaData.Description);

            builder.Property(e => e.Route)
                .HasColumnOrder(3)
                .HasMaxLength(Option.RouteMaxLength)
                .IsRequired()
                .HasComment(OptionsMetaData.Route);

            builder.Property(e => e.Icon)
                .HasColumnOrder(4)
                .HasMaxLength(Option.IconMaxLength)
                .IsRequired()
                .HasComment(OptionsMetaData.Icon)
                .HasConversion(p => p.Value, p => Icon.From(p));

            builder.Property(e => e.Order)
                .HasColumnOrder(5)
                .IsRequired()
                .HasComment(OptionsMetaData.Order)
                .HasConversion(p => p.Value, p => Order.Create(p).Value);

            builder.Property(e => e.CreatedBy)
                .HasColumnOrder(6)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedBy);

            builder.Property(e => e.CreatedOn)
                .HasColumnOrder(7)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.CreatedOn);

            builder.Property(e => e.UpdatedBy)
                .HasColumnOrder(8)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedBy);

            builder.Property(e => e.UpdatedOn)
                .HasColumnOrder(9)
                .IsRequired(false)
                .HasComment(AuditMetaDataConstants.UpdatedOn);

            builder.Property(p => p.Status)
                .HasColumnOrder(10)
                .IsRequired()
                .HasComment(AuditMetaDataConstants.Status)
                .HasConversion(p => p.Value, p => Status.From(p));

            builder.HasData(
                Option.CreateSeed(
                    id: 1,
                    name: "Dashboard",
                    description: "Dashboard",
                    route: "/dashboard",
                    icon: Icon.Dashboard,
                    order: Order.Create(1).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 2,
                    name: "Registro Facturas",
                    description: "Escanear Facturas",
                    route: "/qr",
                    icon: Icon.QrCodeScanner,
                    order: Order.Create(2).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 3,
                    name: "Operaciones",
                    description: "Escanear Facturas",
                    route: "/operations",
                    icon: Icon.Domain,
                    order: Order.Create(3).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 4,
                    name: "Publicaciones",
                    description: "Publicaciones",
                    route: "/news",
                    icon: Icon.Feed,
                    order: Order.Create(4).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 5,
                    name: "Inversionistas",
                    description: "Inversionistas",
                    route: "/investors",
                    icon: Icon.SupervisorAccount,
                    order: Order.Create(5).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 6,
                    name: "Canjes",
                    description: "Canjes",
                    route: "/cashback",
                    icon: Icon.SupervisorAccount,
                    order: Order.Create(6).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 7,
                    name: "Habitaciones",
                    description: "Habitaciones",
                    route: "/rooms",
                    icon: Icon.BedTime,
                    order: Order.Create(7).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value,
                Option.CreateSeed(
                    id: 8,
                    name: "Configuración",
                    description: "Configuracion de la aplicación",
                    route: "/settings",
                    icon: Icon.Settings,
                    order: Order.Create(8).Value,
                    createdBy: Guid.Parse(IUserSession.UserVirtualCode),
                    createdOn: DateTime.UtcNow,
                    status: Status.Active).Value);
        }
    }
}