using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Operation",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Descripcion o nombre de la operacion",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Descripcion o nombre de la operacion");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_Description",
                table: "Operation",
                column: "Description",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operation_Description",
                table: "Operation");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Operation",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Descripcion o nombre de la operacion",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Descripcion o nombre de la operacion");
        }
    }
}
