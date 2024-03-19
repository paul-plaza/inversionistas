using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlLogo",
                table: "CatalogDetail");

            migrationBuilder.CreateSequence<int>(
                name: "NightsDetailSequence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "CatalogDetail",
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
                table: "CatalogDetail",
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
                table: "CatalogDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "CatalogDetail",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Observación de detalle catálogo",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Observación de detalle catálogo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "CatalogDetail",
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
                table: "CatalogDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateTable(
                name: "NightsDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR NightsDetailSequence", comment: "ID del registro int"),
                    MovementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id de movimiento"),
                    OperationId = table.Column<int>(type: "int", nullable: false, comment: "Id de menú"),
                    RoomId = table.Column<int>(type: "int", nullable: false, comment: "Habitación reservada"),
                    Nights = table.Column<int>(type: "int", nullable: false, comment: "Número de noches reservadas"),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Observación"),
                    DateStart = table.Column<DateTime>(type: "date", nullable: false, comment: "Fecha ingreso"),
                    DateEnd = table.Column<DateTime>(type: "date", nullable: false, comment: "Fecha salida"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NightsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_MOVEMENT_WITH_MANY_NIGHTSDETAILS",
                        column: x => x.MovementId,
                        principalTable: "Movement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NightsDetail_MovementId",
                table: "NightsDetail",
                column: "MovementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NightsDetail");

            migrationBuilder.DropSequence(
                name: "NightsDetailSequence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "CatalogDetail",
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
                table: "CatalogDetail",
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
                table: "CatalogDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "CatalogDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Observación de detalle catálogo",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Observación de detalle catálogo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "CatalogDetail",
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
                table: "CatalogDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<string>(
                name: "UrlLogo",
                table: "CatalogDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Ruta del logo del detalle catálogo")
                .Annotation("Relational:ColumnOrder", 5);
        }
    }
}
