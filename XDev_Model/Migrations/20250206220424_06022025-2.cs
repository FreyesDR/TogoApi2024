using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _060220252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypePerson",
                table: "SaleOrderSporadicPartner",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypePerson",
                table: "InvoiceSporadicPartner",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7f23fd99-37f6-46a2-b9d1-0841d0e6b9b6", "7942f235-0a32-4a93-9868-fd1cf426110b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypePerson",
                table: "SaleOrderSporadicPartner");

            migrationBuilder.DropColumn(
                name: "TypePerson",
                table: "InvoiceSporadicPartner");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "97e0065c-010d-4400-bc89-d02cdfb9ade2", "053567bb-d46f-4207-9597-d6afa4ee658f" });
        }
    }
}
