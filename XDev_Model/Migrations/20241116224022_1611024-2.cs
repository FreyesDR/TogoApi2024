using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _16110242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "SaleOrderPosition");

            migrationBuilder.DropColumn(
                name: "Invoiced",
                table: "SaleOrderPosition");

            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "SaleOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Invoiced",
                table: "SaleOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aef5d10a-a6e7-45dd-8f63-efe1cc69c579", "4421413b-a0bd-4896-81ad-690663f2b46a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "SaleOrder");

            migrationBuilder.DropColumn(
                name: "Invoiced",
                table: "SaleOrder");

            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "SaleOrderPosition",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Invoiced",
                table: "SaleOrderPosition",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6f3cf478-6a7e-48c5-8a7e-629d86b37aaf", "7dcb9c4b-728a-49c2-b76e-c596965a7b88" });
        }
    }
}
