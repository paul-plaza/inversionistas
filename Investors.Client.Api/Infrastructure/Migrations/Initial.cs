#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "CatalogDetailSequence");

            migrationBuilder.CreateSequence<int>(
                name: "CatalogSequence");

            migrationBuilder.CreateSequence<int>(
                name: "CategorySequence");

            migrationBuilder.CreateSequence<int>(
                name: "EventDetailSequence");

            migrationBuilder.CreateSequence<int>(
                name: "EventSequence");

            migrationBuilder.CreateSequence<int>(
                name: "EventSubDetailSequence");

            migrationBuilder.CreateSequence<int>(
                name: "InvestorSequence");

            migrationBuilder.CreateSequence<int>(
                name: "MenuSequence");

            migrationBuilder.CreateSequence<int>(
                name: "MenuTypeSequence");

            migrationBuilder.CreateSequence<int>(
                name: "OperationSequence");

            migrationBuilder.CreateSequence<int>(
                name: "ProfileSequence");

            migrationBuilder.CreateSequence<int>(
                name: "RestaurantSequence");

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR CatalogSequence", comment: "ID del registro int"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del menú"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR EventSequence", comment: "ID del registro int"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "Orden del evento"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del menú"),
                    ItemType = table.Column<int>(type: "int", nullable: false, comment: "Tipo de item a mostrar"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR InvestorSequence", comment: "ID del registro int"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Nombre del cliente"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Apellido del cliente"),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Identificacion del cliente"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR OperationSequence", comment: "ID del registro int"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "Orden de la operacion"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre de la operacion"),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Alias de la operacion"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ruta del logo de la operacion"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID del registro Guid"),
                    Identification = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Identificacion del usuario"),
                    DisplayName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Nombre a mostrar en aplicacion y reportes"),
                    UserType = table.Column<int>(type: "int", nullable: false, comment: "Tipo de usuario, administrador, inversionista, invitado"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR CatalogDetailSequence", comment: "ID del registro int"),
                    CatalogId = table.Column<int>(type: "int", nullable: false, comment: "Id de Catálogo a la que pertenece este detalle"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del detalle catálogo"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Valor de detalle catálogo"),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Observación de detalle catálogo"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ruta del logo del detalle catálogo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_CATALOG_WITH_MANY_DETAILS",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR CategorySequence", comment: "ID del registro int"),
                    CatalogId = table.Column<int>(type: "int", nullable: false, comment: "Id de Catálogo a la que pertenece esta categoría"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del detalle catálogo"),
                    Points = table.Column<int>(type: "int", nullable: false, comment: "Puntos para redimir en restaurants"),
                    NextLevelPoints = table.Column<int>(type: "int", nullable: false, comment: "Puntos requeridos para subir de categoría"),
                    Nights = table.Column<int>(type: "int", nullable: false, comment: "Noches para redimir hospedajes"),
                    NextLevelNights = table.Column<int>(type: "int", nullable: false, comment: "Noches requeridas para subir de categoría"),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Observación de detalle catálogo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_CATALOG_WITH_MANY_CATEGORIES",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR EventDetailSequence", comment: "ID del registro int"),
                    EventId = table.Column<int>(type: "int", nullable: false, comment: "Id de Evento a la que pertenece este detalle"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Titulo del item"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del menú"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_EVENT_WITH_MANY_EVENT_DETAILS",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR RestaurantSequence", comment: "ID del registro int"),
                    OperationId = table.Column<int>(type: "int", nullable: false, comment: "Id de Operacion a la que pertenece"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del restaurante"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ruta del logo del restaurante"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_OPERATION_WITH_MANY_RESTAURANTS",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR ProfileSequence", comment: "ID del registro int"),
                    CashBackToRedeem = table.Column<long>(type: "bigint", nullable: false, comment: "Dinero a redimir"),
                    AccumulativeNights = table.Column<long>(type: "bigint", nullable: false, comment: "Noches acumuladas"),
                    NigthsToRedeem = table.Column<long>(type: "bigint", nullable: false, comment: "Número de noches a redimir"),
                    HistoryCashBack = table.Column<long>(type: "bigint", nullable: false, comment: "Historial de dinero canjeado"),
                    HistoryNights = table.Column<int>(type: "int", nullable: false, comment: "Historial noches canjeadas"),
                    Category = table.Column<int>(type: "int", nullable: false, comment: "Categoría del cliente, fourstars, fivestars, sixstars"),
                    TotalAccumulatedInvoice = table.Column<int>(type: "int", nullable: false, comment: "Total de facturas acumuladas"),
                    TotalMonthlyCashBackToRedeem = table.Column<int>(type: "int", nullable: false, comment: "Total mensual de dinero a redimir"),
                    TotalMonthlyCashBackClaimed = table.Column<int>(type: "int", nullable: false, comment: "Total mensual de dinero reclamado"),
                    TotalCashBackClaimed = table.Column<int>(type: "int", nullable: false, comment: "Total de dinero reclamado"),
                    TotalMonthlyNightsClaimed = table.Column<int>(type: "int", nullable: false, comment: "Total mensual de noches reclamadas"),
                    TotalMonthlyNightsToReedem = table.Column<int>(type: "int", nullable: false, comment: "Total mensual de noches a redimir"),
                    TotalNightsClaimed = table.Column<int>(type: "int", nullable: false, comment: "Total de noches reclamadas"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_USER_WITH_ONE_PROFILE",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSubDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR EventSubDetailSequence", comment: "ID del registro int"),
                    EventDetailId = table.Column<int>(type: "int", nullable: false, comment: "Id de evento detalle a la que pertenece este sub detalle"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Título o nombre del detalle beneficio"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Descripción de detalle beneficio"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Ruta del logo del detalle beneficio"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Imagen"),
                    UrlToOpen = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "link de imagen a mostrar"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSubDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_EVENT_DETAIL_WITH_MANY_EVENT_SUB_DETAILS",
                        column: x => x.EventDetailId,
                        principalTable: "EventDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MenuTypeSequence", comment: "ID del registro int"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false, comment: "Id de Restaurante al que pertenece"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del restaurante"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ruta del logo del restaurante"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_RESTAURANT_WITH_MANY_MENUTYPES",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MenuSequence", comment: "ID del registro int"),
                    MenuTypeId = table.Column<int>(type: "int", nullable: false, comment: "Id de tipo de menu al que pertenece"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripcion o nombre del menú"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Título del menú"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ruta del logo del menú"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_TYPEMENU_WITH_MANY_MENUS",
                        column: x => x.MenuTypeId,
                        principalTable: "MenuType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogDetail_CatalogId",
                table: "CatalogDetail",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CatalogId",
                table: "Category",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetail_EventId",
                table: "EventDetail",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubDetail_EventDetailId",
                table: "EventSubDetail",
                column: "EventDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuTypeId",
                table: "Menu",
                column: "MenuTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuType_RestaurantId",
                table: "MenuType",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_UserId",
                table: "Profile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_OperationId",
                table: "Restaurant",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogDetail");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "EventSubDetail");

            migrationBuilder.DropTable(
                name: "Investor");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "EventDetail");

            migrationBuilder.DropTable(
                name: "MenuType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropSequence(
                name: "CatalogDetailSequence");

            migrationBuilder.DropSequence(
                name: "CatalogSequence");

            migrationBuilder.DropSequence(
                name: "CategorySequence");

            migrationBuilder.DropSequence(
                name: "EventDetailSequence");

            migrationBuilder.DropSequence(
                name: "EventSequence");

            migrationBuilder.DropSequence(
                name: "EventSubDetailSequence");

            migrationBuilder.DropSequence(
                name: "InvestorSequence");

            migrationBuilder.DropSequence(
                name: "MenuSequence");

            migrationBuilder.DropSequence(
                name: "MenuTypeSequence");

            migrationBuilder.DropSequence(
                name: "OperationSequence");

            migrationBuilder.DropSequence(
                name: "ProfileSequence");

            migrationBuilder.DropSequence(
                name: "RestaurantSequence");
        }
    }
}
