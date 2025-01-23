using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _160120252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialBranch_Branch_BranchId",
                table: "MaterialBranch");

            migrationBuilder.DropIndex(
                name: "IX_MaterialBranch_BranchId",
                table: "MaterialBranch");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f95b8e65-06f0-4b3e-8aaf-1b60b93a0e79", "f9d5e32c-0242-4218-b47e-795b536edf43" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8a786b64-93ef-4a8a-ab46-af47e43801be", "9e119100-ac11-4cef-a062-7dc3a2a878a2" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBranch_BranchId",
                table: "MaterialBranch",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialBranch_Branch_BranchId",
                table: "MaterialBranch",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
