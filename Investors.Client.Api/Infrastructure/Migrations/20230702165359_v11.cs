using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Notification");

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Tipo de notificación")
                .Annotation("Relational:ColumnOrder", 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Notification",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Imagen de la notificación")
                .Annotation("Relational:ColumnOrder", 5);
        }
    }
}
