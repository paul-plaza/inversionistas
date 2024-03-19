using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ONE_INVOICE_WITH_MANY_INVOICEDETAILS",
                table: "InvoiceDetail");

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
                .OldAnnotation("Relational:ColumnOrder", 11);

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
                .OldAnnotation("Relational:ColumnOrder", 10);

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
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceType",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "Tipo de factura (Cashback,Alojamiento,Mixto)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de factura (Cashback,Alojamiento,Mixto)")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationInvestor",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Identificacion del Inversionista",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Identificacion de inversionista");

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Identificacion registrada en la factura",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Identificacion de referido");

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
                .OldAnnotation("Relational:ColumnOrder", 9);

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
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Id de la operación donde se realizo el consumo")
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_INVOICE_WITH_MANY_INVOICE_DETAILS",
                table: "InvoiceDetail",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ONE_INVOICE_WITH_MANY_INVOICE_DETAILS",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "Invoice");

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
                .Annotation("Relational:ColumnOrder", 11)
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
                .Annotation("Relational:ColumnOrder", 10)
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
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 13);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceType",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "Tipo de factura (Cashback,Alojamiento,Mixto)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de factura (Cashback,Alojamiento,Mixto)")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationInvestor",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Identificacion de inversionista",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Identificacion del Inversionista");

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Identificacion de referido",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Identificacion registrada en la factura");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 9)
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
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_INVOICE_WITH_MANY_INVOICEDETAILS",
                table: "InvoiceDetail",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
