using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nights",
                table: "Room");

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Observación de la habitación")
                .Annotation("Relational:ColumnOrder", 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Valor en noches de la habitación")
                .Annotation("Relational:ColumnOrder", 5);
        }
    }
}
