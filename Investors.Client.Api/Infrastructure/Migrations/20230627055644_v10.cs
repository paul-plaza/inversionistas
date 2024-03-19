using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Movement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Total del valor acumulado en noches o puntos")
                .Annotation("Relational:ColumnOrder", 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Movement");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
