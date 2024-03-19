using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "RoomSequence");

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR RoomSequence", comment: "ID del registro int"),
                    OperationId = table.Column<int>(type: "int", nullable: false, comment: "Id de Operacion a la que pertenece"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripción de habitación"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Título de habitación"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ruta del logo de habitación"),
                    Nights = table.Column<int>(type: "int", nullable: false, comment: "Valor en noches de la habitación"),
                    Guests = table.Column<int>(type: "int", nullable: false, comment: "Número máximo de huéspedes por habitación"),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Tipo de habitación"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_OPERATION_WITH_MANY_ROOMS",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_OperationId",
                table: "Room",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropSequence(
                name: "RoomSequence");
        }
    }
}
