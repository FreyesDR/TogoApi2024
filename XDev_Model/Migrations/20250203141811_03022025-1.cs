using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _030220251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContacPersonPhone",
                table: "Partner",
                newName: "ContactPersonPhone");

            migrationBuilder.RenameColumn(
                name: "ContacPersonName",
                table: "Partner",
                newName: "ContactPersonName");

            migrationBuilder.RenameColumn(
                name: "ContacPersonIDNumber",
                table: "Partner",
                newName: "ContactPersonIDNumber");

            migrationBuilder.RenameColumn(
                name: "ContacPersonEmail",
                table: "Partner",
                newName: "ContactPersonEmail");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1568e119-eaf0-49f1-afea-2ad9a50ce593", "af5e190e-e900-4f65-b532-b00cff892edf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactPersonPhone",
                table: "Partner",
                newName: "ContacPersonPhone");

            migrationBuilder.RenameColumn(
                name: "ContactPersonName",
                table: "Partner",
                newName: "ContacPersonName");

            migrationBuilder.RenameColumn(
                name: "ContactPersonIDNumber",
                table: "Partner",
                newName: "ContacPersonIDNumber");

            migrationBuilder.RenameColumn(
                name: "ContactPersonEmail",
                table: "Partner",
                newName: "ContacPersonEmail");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "89544389-d6be-4909-a137-783a8ca60b4e", "6ba9803d-a922-487b-9f6a-3a55ab839e1e" });
        }
    }
}
