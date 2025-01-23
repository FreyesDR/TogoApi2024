using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _210120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EBilling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlTest = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlProd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlSigner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBilling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EBillingCompany",
                columns: table => new
                {
                    EBillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsProd = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nif1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nif2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingCompany", x => new { x.EBillingId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_EBillingCompany_EBilling_EBillingId",
                        column: x => x.EBillingId,
                        principalTable: "EBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EBilling",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "UrlProd", "UrlSigner", "UrlTest" },
                values: new object[] { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ministerio de Hacienda de El Salvador", "https://api.dtes.mh.gob.sv", "http://localhost:8113/", "https://apitest.dtes.mh.gob.sv" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "79cf651f-c824-4ca1-891a-1941d8e9ee9c", "42b93e5b-8117-4afd-af41-b5a440ec019b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EBillingCompany");

            migrationBuilder.DropTable(
                name: "EBilling");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7df5621d-a5a5-493d-840a-ab037c915d11", "37791afc-43e9-42b2-a69f-1b7a5b2c9e25" });
        }
    }
}
