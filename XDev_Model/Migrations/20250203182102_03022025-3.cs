using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _030220253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodGeneración",
                table: "Invoice",
                newName: "CodGeneracion");

            migrationBuilder.CreateTable(
                name: "EBillingLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointSaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodGen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumControl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SelloRecibido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TipoDte = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ResponseStatusCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingLog", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e3160019-e966-40b6-bbf7-bf0be6b583e4", "b9175972-a69f-446f-a77f-7a476c548f65" });

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_BranchId",
                table: "EBillingLog",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_CodGen",
                table: "EBillingLog",
                column: "CodGen");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_CompanyId",
                table: "EBillingLog",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EBillingLog_PointSaleId",
                table: "EBillingLog",
                column: "PointSaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EBillingLog");

            migrationBuilder.RenameColumn(
                name: "CodGeneracion",
                table: "Invoice",
                newName: "CodGeneración");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2358517-688d-4f6c-a5dd-7d1fac9ad956", "c64fceb8-b848-416f-96b3-302924698ddb" });
        }
    }
}
