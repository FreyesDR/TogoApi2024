using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _200220251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromName",
                table: "EBillingCompany",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SmtpEnableSsl",
                table: "EBillingCompany",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SmtpHost",
                table: "EBillingCompany",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmtpPort",
                table: "EBillingCompany",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SmtpService",
                table: "EBillingCompany",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpUserName",
                table: "EBillingCompany",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpUserPassword",
                table: "EBillingCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "89da995a-f93f-4aa4-b55d-74897dc98591", "4af5d31e-de2e-45a2-b37d-5eb1d32ff98c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromName",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "SmtpEnableSsl",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "SmtpHost",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "SmtpPort",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "SmtpService",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "SmtpUserName",
                table: "EBillingCompany");

            migrationBuilder.DropColumn(
                name: "SmtpUserPassword",
                table: "EBillingCompany");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b88ca03d-36d8-4be3-be20-5eeb2c8690b4", "77cf37ae-9ed9-40ba-8fb2-b135bf14afbb" });
        }
    }
}
