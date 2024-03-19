using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OptionSequence");

            migrationBuilder.CreateSequence<int>(
                name: "UserAdministratorOperationSequence");

            migrationBuilder.CreateSequence<int>(
                name: "UserAdministratorOptionSequence");

            migrationBuilder.CreateSequence<int>(
                name: "UserAdministratorSequence");

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR OptionSequence", comment: "ID del registro Int"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Nombre de opcion menu"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Descripcion de opcion menu"),
                    Route = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Ruta de opcion menu"),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Icono de opcion menu"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "Orden de opcion menu"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAdministrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UserAdministratorSequence", comment: "ID del registro Int"),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "Email registrado en portal Azure Directorio Activo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdministrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAdministratorOperation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UserAdministratorOperationSequence", comment: "ID del registro Int"),
                    UserAdministratorId = table.Column<int>(type: "int", nullable: false, comment: "Id del usuario administrador"),
                    OperationId = table.Column<int>(type: "int", nullable: false, comment: "Id de la opcion de menu"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdministratorOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_USER_ADMINISTRATOR_WITH_MANY_OPERATIONS",
                        column: x => x.UserAdministratorId,
                        principalTable: "UserAdministrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAdministratorOperation_Operation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAdministratorOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UserAdministratorOptionSequence", comment: "ID del registro Int"),
                    UserAdministratorId = table.Column<int>(type: "int", nullable: false, comment: "Id del usuario administrador"),
                    OptionId = table.Column<int>(type: "int", nullable: false, comment: "Id de la opcion de menu"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdministratorOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_USER_ADMINISTRATOR_WITH_MANY_MENUS",
                        column: x => x.UserAdministratorId,
                        principalTable: "UserAdministrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAdministratorOption_Option_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Option",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Option",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "Icon", "Name", "Order", "Route", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6170), "Dashboard", "dashboard", "Dashboard", 1, "/dashboard", 1, null, null },
                    { 2, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6190), "Escanear Facturas", "qr_code_scanner", "Registro Facturas", 2, "/qr", 1, null, null },
                    { 3, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6200), "Escanear Facturas", "domain", "Operaciones", 3, "/operations", 1, null, null },
                    { 4, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6200), "Publicaciones", "feed", "Publicaciones", 4, "/news", 1, null, null },
                    { 5, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6200), "Inversionistas", "supervisor_account", "Inversionistas", 5, "/investors", 1, null, null },
                    { 6, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6200), "Canjes", "supervisor_account", "Canjes", 6, "/cashback", 1, null, null },
                    { 7, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6210), "Habitaciones", "bedtime", "Habitaciones", 7, "/rooms", 1, null, null },
                    { 8, new Guid("850f91cd-a853-45be-b677-0864ca160e0e"), new DateTime(2023, 11, 3, 16, 22, 14, 742, DateTimeKind.Utc).AddTicks(6210), "Configuracion de la aplicación", "settings", "Configuración", 8, "/settings", 1, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAdministratorOperation_OperationId",
                table: "UserAdministratorOperation",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdministratorOperation_UserAdministratorId",
                table: "UserAdministratorOperation",
                column: "UserAdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdministratorOption_OptionId",
                table: "UserAdministratorOption",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdministratorOption_UserAdministratorId",
                table: "UserAdministratorOption",
                column: "UserAdministratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAdministratorOperation");

            migrationBuilder.DropTable(
                name: "UserAdministratorOption");

            migrationBuilder.DropTable(
                name: "UserAdministrator");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropSequence(
                name: "OptionSequence");

            migrationBuilder.DropSequence(
                name: "UserAdministratorOperationSequence");

            migrationBuilder.DropSequence(
                name: "UserAdministratorOptionSequence");

            migrationBuilder.DropSequence(
                name: "UserAdministratorSequence");
        }
    }
}
