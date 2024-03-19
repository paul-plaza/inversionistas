using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSync",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "NigthsToRedeem",
                table: "Profile",
                newName: "NightsToRedeem");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceType",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "Tipo de factura (Cashback,Alojamiento,Mixto)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de factura");

            migrationBuilder.AddColumn<double>(
                name: "TotalInvoice",
                table: "Invoice",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Total de factua, es la suma de cada item del detalle")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInvoice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "NightsToRedeem",
                table: "Profile",
                newName: "NigthsToRedeem");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceType",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "Tipo de factura",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de factura (Cashback,Alojamiento,Mixto)");

            migrationBuilder.AddColumn<bool>(
                name: "IsSync",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Es sincronizada")
                .Annotation("Relational:ColumnOrder", 6);
        }
    }
}
