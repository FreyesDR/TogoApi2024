using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _140220251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SentEmail",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f55ba1bc-47ef-4dc4-a368-a4a6de8410c2", "b1c7add4-6132-49a0-a557-98608c7cb2f5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentEmail",
                table: "Invoice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c244074a-26f5-4e4a-b867-e3d13081341f", "ee70b11d-4ba8-44ec-bcd2-aab702c6c348" });
        }
    }
}
