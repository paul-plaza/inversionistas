using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_MANY_CASHBACKDETAILS",
                table: "CashbackDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_MANY_NIGHTSDETAILS",
                table: "NightsDetail");

            migrationBuilder.DropIndex(
                name: "IX_NightsDetail_MovementId",
                table: "NightsDetail");

            migrationBuilder.DropColumn(
                name: "Nights",
                table: "NightsDetail");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "NightsDetail");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "NightsDetail");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Movement",
                newName: "TotalToRedeem");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Movement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Movement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Movement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Total de articulos en el pedido")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateIndex(
                name: "IX_NightsDetail_MovementId",
                table: "NightsDetail",
                column: "MovementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_MANY_CASHBACK_DETAILS",
                table: "CashbackDetail",
                column: "MovementId",
                principalTable: "Movement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_ONE_NIGHTS_DETAILS",
                table: "NightsDetail",
                column: "MovementId",
                principalTable: "Movement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_MANY_CASHBACK_DETAILS",
                table: "CashbackDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_ONE_NIGHTS_DETAILS",
                table: "NightsDetail");

            migrationBuilder.DropIndex(
                name: "IX_NightsDetail_MovementId",
                table: "NightsDetail");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Movement");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Movement");

            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Movement");

            migrationBuilder.RenameColumn(
                name: "TotalToRedeem",
                table: "Movement",
                newName: "Total");

            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "NightsDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Número de noches reservadas")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                table: "NightsDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Id de menú")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "NightsDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Habitación reservada")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Movement",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.CreateIndex(
                name: "IX_NightsDetail_MovementId",
                table: "NightsDetail",
                column: "MovementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_MANY_CASHBACKDETAILS",
                table: "CashbackDetail",
                column: "MovementId",
                principalTable: "Movement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_MOVEMENT_WITH_MANY_NIGHTSDETAILS",
                table: "NightsDetail",
                column: "MovementId",
                principalTable: "Movement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
