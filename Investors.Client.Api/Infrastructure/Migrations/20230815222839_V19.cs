using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "User",
                type: "int",
                nullable: false,
                comment: "Tipo de usuario, administrador, inversionista, invitado",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de usuario, administrador, inversionista, invitado")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "User",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 11)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "User",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 12)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Identificacion del usuario",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Identificacion del usuario")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Email registrado en Azure B2C",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Email registrado en Azure B2C")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Nombre a mostrar en aplicacion y reportes")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<string>(
                name: "SurName",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Nombre a mostrar en aplicacion y reportes")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddForeignKey(
                name: "FK_ONE_USER_WITH_MANY_NOTIFICATIONS",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ONE_USER_WITH_MANY_NOTIFICATIONS",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SurName",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "User",
                type: "int",
                nullable: false,
                comment: "Tipo de usuario, administrador, inversionista, invitado",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Tipo de usuario, administrador, inversionista, invitado")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "User",
                type: "datetime2",
                nullable: true,
                comment: "Fecha de ultima actualizacion del registro",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Fecha de ultima actualizacion del registro")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 11);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Usuario que modifica el registro",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Usuario que modifica el registro")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "User",
                type: "int",
                nullable: false,
                comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 12);

            migrationBuilder.AlterColumn<string>(
                name: "Identification",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Identificacion del usuario",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Identificacion del usuario")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Email registrado en Azure B2C",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Email registrado en Azure B2C")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                type: "datetime2",
                nullable: false,
                comment: "Fecha de creacion",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Fecha de creacion")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Usuario creador",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Usuario creador")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
