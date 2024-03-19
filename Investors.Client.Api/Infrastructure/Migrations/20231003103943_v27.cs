using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IDX_UNIQUE_VALUES_IDENTIFICATION",
                table: "User",
                columns: new[] { "Identification", "Status" },
                unique: true,
                filter: "Status = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_UNIQUE_VALUES_IDENTIFICATION",
                table: "User");
        }
    }
}
