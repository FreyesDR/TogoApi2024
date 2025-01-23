using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _160120253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_MaterialWareHouse_MaterialBranchId",
                table: "MaterialWareHouse");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MaterialWareHouse");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "MaterialWareHouse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse",
                columns: new[] { "MaterialBranchId", "WareHouseId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "37e663fb-6f4a-410a-b54d-763d938f1677", "971ab3a3-7b87-4905-b043-9b78e605cdce" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MaterialWareHouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "MaterialWareHouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f95b8e65-06f0-4b3e-8aaf-1b60b93a0e79", "f9d5e32c-0242-4218-b47e-795b536edf43" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialWareHouse_MaterialBranchId",
                table: "MaterialWareHouse",
                column: "MaterialBranchId");
        }
    }
}
