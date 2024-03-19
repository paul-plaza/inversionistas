using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR NotificationSequence",
                comment: "ID del registro int",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ID del registro int")
                ;

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InvoiceDetail",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR InvoiceDetailSequence",
                comment: "ID del registro int",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ID del registro int")
                ;

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR InvoiceSequence",
                comment: "ID del registro int",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ID del registro int")
                ;

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6920));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6920));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6940));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 26, 21, 21, 24, 451, DateTimeKind.Utc).AddTicks(6940));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Notification",
                type: "int",
                nullable: false,
                comment: "ID del registro int",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR NotificationSequence",
                oldComment: "ID del registro int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InvoiceDetail",
                type: "int",
                nullable: false,
                comment: "ID del registro int",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR InvoiceDetailSequence",
                oldComment: "ID del registro int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Invoice",
                type: "int",
                nullable: false,
                comment: "ID del registro int",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR InvoiceSequence",
                oldComment: "ID del registro int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3390));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Option",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2023, 11, 6, 23, 36, 17, 916, DateTimeKind.Utc).AddTicks(3410));
        }
    }
}