using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OperationId",
                table: "Notification",
                type: "int",
                nullable: true,
                comment: "Id de la operación",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id de la operación");

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7140));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 28, 14, 52, 49, 865, DateTimeKind.Utc).AddTicks(7170));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OperationId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Id de la operación",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Id de la operación");

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
    }
}
