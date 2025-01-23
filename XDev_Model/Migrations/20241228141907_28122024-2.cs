using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _281220242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                table: "PriceSchemeCondition");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b5224ba3-7b2d-4ebd-bedd-5c886e7b283b", "93ebbf6a-05f3-4493-86b3-50998825f1ca" });

            migrationBuilder.AddForeignKey(
                name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                table: "PriceSchemeCondition",
                column: "PriceConditionId",
                principalTable: "PriceCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                table: "PriceSchemeCondition");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6d02496d-9984-4081-9ad9-a0b5a97d7d4b", "42e1397e-62cb-4bc5-a11c-7e80c10890bf" });

            migrationBuilder.AddForeignKey(
                name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                table: "PriceSchemeCondition",
                column: "PriceConditionId",
                principalTable: "PriceCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
