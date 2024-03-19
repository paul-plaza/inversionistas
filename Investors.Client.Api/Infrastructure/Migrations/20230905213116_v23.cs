using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Tipo de habitación")
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "Room");

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Tipo de habitación")
                .Annotation("Relational:ColumnOrder", 7);
        }
    }
}
