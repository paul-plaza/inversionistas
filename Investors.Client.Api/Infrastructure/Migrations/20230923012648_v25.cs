using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Profile",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Profile",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Profile",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 19)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Profile",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 15);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Profile",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 14);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstInvoiceRegistrationDate",
                table: "Profile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Fecha de la primera factura registrada")
                .Annotation("Relational:ColumnOrder", 14);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstInvoiceRegistrationDate",
                table: "Profile");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Profile",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 17)
                .OldAnnotation("Relational:ColumnOrder", 18);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Profile",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 16)
                .OldAnnotation("Relational:ColumnOrder", 17);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Profile",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 18)
                .OldAnnotation("Relational:ColumnOrder", 19);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Profile",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 15)
                .OldAnnotation("Relational:ColumnOrder", 16);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Profile",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 14)
                .OldAnnotation("Relational:ColumnOrder", 15);
        }
    }
}
