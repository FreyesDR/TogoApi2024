using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _240120253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentConditionId",
                table: "SaleOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentConditionId",
                table: "Partner",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentConditionId",
                table: "Invoice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Plazo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    PlazoCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeanOfPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeanOfPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeanOfPayment_PaymentCondition_PaymentConditionId",
                        column: x => x.PaymentConditionId,
                        principalTable: "PaymentCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8544ea20-f5da-4e11-8140-ad939d5d16d7", "ff2e579e-69b6-4bc8-bc3d-05b6d2c10802" });

            migrationBuilder.CreateIndex(
                name: "IX_Partner_PaymentConditionId",
                table: "Partner",
                column: "PaymentConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_MeanOfPayment_Code",
                table: "MeanOfPayment",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_MeanOfPayment_PaymentConditionId",
                table: "MeanOfPayment",
                column: "PaymentConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCondition_Code",
                table: "PaymentCondition",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_PaymentCondition_PaymentConditionId",
                table: "Partner",
                column: "PaymentConditionId",
                principalTable: "PaymentCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partner_PaymentCondition_PaymentConditionId",
                table: "Partner");

            migrationBuilder.DropTable(
                name: "MeanOfPayment");

            migrationBuilder.DropTable(
                name: "PaymentCondition");

            migrationBuilder.DropIndex(
                name: "IX_Partner_PaymentConditionId",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "PaymentConditionId",
                table: "SaleOrder");

            migrationBuilder.DropColumn(
                name: "PaymentConditionId",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "PaymentConditionId",
                table: "Invoice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "97571ac9-176e-4694-b448-7869cbf95d8a", "de5ec355-645d-4088-8657-77e61db41a62" });
        }
    }
}
