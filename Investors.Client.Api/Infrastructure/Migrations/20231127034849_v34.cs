using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "InvoiceDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "InvoiceDetail",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "InvoiceDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "InvoiceDetail",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "InvoiceDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "InvoiceDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de entrada")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "InvoiceDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de salida")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "InvoiceDetail",
                type: "int",
                nullable: true,
                comment: "Noches acumuladas en el registro")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "InvoiceDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Tipo de registro (Cashback, Alojamiento, Mixto)")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoice",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<long>(
                name: "TotalNights",
                table: "Invoice",
                type: "bigint",
                nullable: true,
                comment: "Total de noches generados en la factura")
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddColumn<long>(
                name: "TotalPoints",
                table: "Invoice",
                type: "bigint",
                nullable: true,
                comment: "Total de puntos generados en la factura")
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7190));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 3, 48, 49, 146, DateTimeKind.Utc).AddTicks(7220));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "Nights",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "TotalNights",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalPoints",
                table: "Invoice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "InvoiceDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "InvoiceDetail",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "InvoiceDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "InvoiceDetail",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "InvoiceDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Invoice",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 13)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6920));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6920));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6940));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6940));
        }
    }
}
