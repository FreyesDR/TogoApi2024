using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _020120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SourceConditionId",
                table: "PriceCondition",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SaleOrderPositionCondition",
                columns: table => new
                {
                    SaleOrderPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SourceConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValueType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    BaseCondition = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValueCondition = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderPositionCondition", x => new { x.SaleOrderPositionId, x.PriceConditionId });
                    table.ForeignKey(
                        name: "FK_SaleOrderPositionCondition_SaleOrderPosition_SaleOrderPositionId",
                        column: x => x.SaleOrderPositionId,
                        principalTable: "SaleOrderPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2674f3d3-e3f7-416f-a7de-dd659e985768", "0a2d4246-a498-43d8-82ab-85a5d20d21b7" });

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderPositionCondition_Code",
                table: "SaleOrderPositionCondition",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleOrderPositionCondition");

            migrationBuilder.DropColumn(
                name: "SourceConditionId",
                table: "PriceCondition");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b5224ba3-7b2d-4ebd-bedd-5c886e7b283b", "93ebbf6a-05f3-4493-86b3-50998825f1ca" });
        }
    }
}
