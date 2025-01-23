using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _220120254 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EBillingDocument",
                columns: table => new
                {
                    EBillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBillingDocument", x => new { x.EBillingId, x.Id });
                    table.ForeignKey(
                        name: "FK_EBillingDocument_EBilling_EBillingId",
                        column: x => x.EBillingId,
                        principalTable: "EBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EBillingDocument",
                columns: new[] { "EBillingId", "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("1dc5c31b-adb6-40b7-8523-5a91ecaea5a3"), "05", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nota de crédito" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("1f0e23ab-5e6b-4fca-936e-fb1ea18b40af"), "11", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Factura exportador" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("6d339ff2-e58c-4eba-95e9-dd0df0d1abbe"), "01", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Factura" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("9ca389cf-65aa-4b54-bb1b-0080efaa6cb2"), "06", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nota de débito" },
                    { new Guid("63be85df-6805-41c3-beb2-f6a44db746f6"), new Guid("d4a32bf7-a3dd-4623-81f4-5a9994e6c9d0"), "03", "24dd7daf-bc2c-4a25-9a8e-c056b4eb1a8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Comprobante crédito fiscal" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "27d52fc0-c85b-4d85-b1a5-20057bdc981d", "ab9919e0-8598-45b3-9a39-51e994b3fa78" });

            migrationBuilder.CreateIndex(
                name: "IX_EBillingDocument_Code",
                table: "EBillingDocument",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EBillingDocument");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d121b046-bdc9-4977-b991-dc1a03a1a77a", "e871b529-0aef-43a3-9ed7-8ad970786a57" });
        }
    }
}
