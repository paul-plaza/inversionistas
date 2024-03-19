using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "CashbackDetailSequence");

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID del registro Guid"),
                    OperationId = table.Column<int>(type: "int", nullable: false, comment: "Id de operación"),
                    TransactionType = table.Column<int>(type: "int", nullable: false, comment: "Tipo de transacción"),
                    TransactionState = table.Column<int>(type: "int", nullable: false, comment: "Estado de transacción o movimiento"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_USER_WITH_MANY_MOVEMENTS",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashbackDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR CashbackDetailSequence", comment: "ID del registro int"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false, comment: "Id de restaurant donde registró el movimiento"),
                    MenuId = table.Column<int>(type: "int", nullable: false, comment: "Id de menú"),
                    Points = table.Column<int>(type: "int", nullable: false, comment: "Puntos que costó el movimiento"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)"),
                    MovementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashbackDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_MOVEMENT_WITH_MANY_CASHBACKDETAILS",
                        column: x => x.MovementId,
                        principalTable: "Movement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashbackDetail_MovementId",
                table: "CashbackDetail",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_UserId",
                table: "Movement",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashbackDetail");

            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropSequence(
                name: "CashbackDetailSequence");
        }
    }
}
