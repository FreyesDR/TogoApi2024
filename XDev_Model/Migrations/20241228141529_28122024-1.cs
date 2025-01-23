using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _281220241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaterialType",
                keyColumn: "Id",
                keyValue: new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"),
                column: "Code",
                value: "B");

            migrationBuilder.UpdateData(
                table: "MaterialType",
                keyColumn: "Id",
                keyValue: new Guid("691f3725-3ef8-434b-aecf-b663791cc501"),
                column: "Code",
                value: "S");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6d02496d-9984-4081-9ad9-a0b5a97d7d4b", "42e1397e-62cb-4bc5-a11c-7e80c10890bf" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceSchemeCondition_PriceConditionId",
                table: "PriceSchemeCondition",
                column: "PriceConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                table: "PriceSchemeCondition",
                column: "PriceConditionId",
                principalTable: "PriceCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceSchemeCondition_PriceCondition_PriceConditionId",
                table: "PriceSchemeCondition");

            migrationBuilder.DropIndex(
                name: "IX_PriceSchemeCondition_PriceConditionId",
                table: "PriceSchemeCondition");

            migrationBuilder.UpdateData(
                table: "MaterialType",
                keyColumn: "Id",
                keyValue: new Guid("0f8d6ce6-6177-4892-843a-11e2af8aa134"),
                column: "Code",
                value: "1");

            migrationBuilder.UpdateData(
                table: "MaterialType",
                keyColumn: "Id",
                keyValue: new Guid("691f3725-3ef8-434b-aecf-b663791cc501"),
                column: "Code",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "057826e0-614d-4bb0-86a7-c4cabc0c276f", "b6494338-6e79-44a6-940c-0ea01c4fff29" });
        }
    }
}
