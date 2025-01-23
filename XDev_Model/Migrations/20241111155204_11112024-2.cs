using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _111120242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PartnerFeatures",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "NumType", "RangeId" },
                values: new object[] { new Guid("7ca98566-a39c-41fc-bd63-237dd34eb344"), "7ca98566-a39c-41fc-bd63-237dd34eb344", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)0, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e86b219a-92c7-4494-8616-43edfe746f6e", "96b80300-c5a6-4e78-9401-20e0eeb5affd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartnerFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7ca98566-a39c-41fc-bd63-237dd34eb344"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "63d1fb16-7a3f-4da5-9eb5-66698644dbe4", "038fd622-5f5a-48a5-940f-8774a29d2f00" });
        }
    }
}
