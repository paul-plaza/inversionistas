using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Room",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "Título de habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Título de habitación");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "Room",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "Observación de la habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Observación de la habitación");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Room",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "Descripción de habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Descripción de habitación");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Título de habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "Título de habitación");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "Room",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Observación de la habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "Observación de la habitación");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Descripción de habitación",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "Descripción de habitación");
        }
    }
}
