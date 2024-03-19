using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "CashbackDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "NightsDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "NightsDetail",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "NightsDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "NightsDetail",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Observación",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Observación")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "NightsDetail",
                type: "date",
                nullable: false,
                comment: "Fecha ingreso",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldComment: "Fecha ingreso")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "NightsDetail",
                type: "date",
                nullable: false,
                comment: "Fecha salida",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldComment: "Fecha salida")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "NightsDetail",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "NightsDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 8);

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
                .Annotation("Relational:ColumnOrder", 11)
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
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Tipo de transacción",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de transacción")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionState",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado de transacción o movimiento",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado de transacción o movimiento")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "TotalToRedeem",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Total del valor acumulado en noches o puntos",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Total del valor acumulado en noches o puntos")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "TotalItems",
                table: "Movement",
                type: "int",
                nullable: true,
                comment: "Total de articulos en el pedido",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Total de articulos en el pedido")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Movement",
                type: "int",
                nullable: true,
                comment: "Id de la habitación cuando el movimiento es tipo noches",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Movement",
                type: "int",
                nullable: true,
                comment: "Id del restaurante cuando el movimiento es tipo cashback",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "OperationId",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Id de operación",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id de operación")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Movement",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 9)
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
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "CashbackDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "CashbackDetail",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                comment: "Puntos que costó el movimiento",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Puntos que costó el movimiento")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                comment: "Id de menú",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id de menú")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "CashbackDetail",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "CashbackDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "NightsDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "NightsDetail",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "NightsDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "NightsDetail",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Observación",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Observación")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "NightsDetail",
                type: "date",
                nullable: false,
                comment: "Fecha ingreso",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldComment: "Fecha ingreso")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "NightsDetail",
                type: "date",
                nullable: false,
                comment: "Fecha salida",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldComment: "Fecha salida")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "NightsDetail",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "NightsDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 5);

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
                .OldAnnotation("Relational:ColumnOrder", 11);

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
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Tipo de transacción",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de transacción")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionState",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Estado de transacción o movimiento",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado de transacción o movimiento")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "TotalToRedeem",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Total del valor acumulado en noches o puntos",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Total del valor acumulado en noches o puntos")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "TotalItems",
                table: "Movement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Total de articulos en el pedido",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Total de articulos en el pedido")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

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
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Movement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Id de la habitación cuando el movimiento es tipo noches")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Movement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Id del restaurante cuando el movimiento es tipo cashback")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "OperationId",
                table: "Movement",
                type: "int",
                nullable: false,
                comment: "Id de operación",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id de operación")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

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
                .OldAnnotation("Relational:ColumnOrder", 9);

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
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "CashbackDetail",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "CashbackDetail",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                comment: "Puntos que costó el movimiento",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Puntos que costó el movimiento")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                comment: "Id de menú",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id de menú")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "CashbackDetail",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "CashbackDetail",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "CashbackDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Id de restaurant donde registró el movimiento")
                .Annotation("Relational:ColumnOrder", 2);
        }
    }
}
