using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _180120252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialBranch_Material_MaterialId",
                table: "MaterialBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialWareHouse_MaterialBranch_MaterialBranchId",
                table: "MaterialWareHouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialBranch",
                table: "MaterialBranch");

            migrationBuilder.DropIndex(
                name: "IX_MaterialBranch_MaterialId",
                table: "MaterialBranch");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MaterialBranch");

            migrationBuilder.RenameColumn(
                name: "MaterialBranchId",
                table: "MaterialWareHouse",
                newName: "BranchId");

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "MaterialWareHouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse",
                columns: new[] { "MaterialId", "BranchId", "WareHouseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialBranch",
                table: "MaterialBranch",
                columns: new[] { "MaterialId", "BranchId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7df5621d-a5a5-493d-840a-ab037c915d11", "37791afc-43e9-42b2-a69f-1b7a5b2c9e25" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialBranch",
                table: "MaterialBranch");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "MaterialWareHouse");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "MaterialWareHouse",
                newName: "MaterialBranchId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MaterialBranch",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialWareHouse",
                table: "MaterialWareHouse",
                columns: new[] { "MaterialBranchId", "WareHouseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialBranch",
                table: "MaterialBranch",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3f958c07-584f-495c-a60d-a1d1e4575a5b", "5ed5ab5e-f037-4494-a15a-6ea17d5598b1" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBranch_MaterialId",
                table: "MaterialBranch",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialBranch_Material_MaterialId",
                table: "MaterialBranch",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialWareHouse_MaterialBranch_MaterialBranchId",
                table: "MaterialWareHouse",
                column: "MaterialBranchId",
                principalTable: "MaterialBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
