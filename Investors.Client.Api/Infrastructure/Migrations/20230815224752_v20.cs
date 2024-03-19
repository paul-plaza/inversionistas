using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "Investor",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Identificacion del cliente",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Identificacion del cliente");

            migrationBuilder.AlterColumn<string>(
                name: "FullNames",
                table: "Investor",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "Nombre del cliente",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Nombre del cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "Investor",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Identificacion del cliente",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Identificacion del cliente");

            migrationBuilder.AlterColumn<string>(
                name: "FullNames",
                table: "Investor",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Nombre del cliente",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "Nombre del cliente");
        }
    }
}
