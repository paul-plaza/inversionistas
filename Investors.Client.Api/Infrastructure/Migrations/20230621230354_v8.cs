using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investors.Client.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "InvoiceDetailSequence");

            migrationBuilder.CreateSequence<int>(
                name: "InvoiceSequence");

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID del registro int")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<int>(type: "int", nullable: false, comment: "Id del recibo"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Numero de la factura"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de la factura"),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Identificacion de referido"),
                    IdentificationInvestor = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Identificacion de inversionista"),
                    IsSync = table.Column<bool>(type: "bit", nullable: false, comment: "Es sincronizada"),
                    InvoiceType = table.Column<int>(type: "int", nullable: false, comment: "Tipo de factura"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_RECEIPT_WITH_MANY_INVOICES",
                        column: x => x.ReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID del registro int")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false, comment: "Id de la factura"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Grupo de consumo"),
                    GroupDetail = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Descripción del grupo"),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Valor total del item"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Usuario creador"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Fecha de creacion"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Usuario que modifica el registro"),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha de ultima actualizacion del registro"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Estado del registro Activo(1), Inactivo(2), Eliminado(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ONE_INVOICE_WITH_MANY_INVOICEDETAILS",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ReceiptId",
                table: "Invoice",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropSequence(
                name: "InvoiceDetailSequence");

            migrationBuilder.DropSequence(
                name: "InvoiceSequence");
        }
    }
}
