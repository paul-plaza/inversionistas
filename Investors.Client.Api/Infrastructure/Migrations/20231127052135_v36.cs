using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2350));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 21, 34, 908, DateTimeKind.Utc).AddTicks(2380));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9760));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9780));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 27, 5, 16, 36, 723, DateTimeKind.Utc).AddTicks(9790));
        }
    }
}
