using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "NotificationSequence");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "Room",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Observación de la habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Observación de la habitación");

            migrationBuilder.AlterColumn<double>(
                name: "TotalValue",
                table: "InvoiceDetail",
                type: "float",
                nullable: false,
                comment: "Valor total del item",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Valor total del item");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID del registro int")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id del usuario"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Título de la notificación"),
                    SubTitle = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true, comment: "Subtítulo de la notificación"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Descripción de la notificación"),
                    UrlImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Imagen de la notificación"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropSequence(
                name: "NotificationSequence");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Observación de la habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Observación de la habitación");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalValue",
                table: "InvoiceDetail",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Valor total del item",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Valor total del item");
        }
    }
}
