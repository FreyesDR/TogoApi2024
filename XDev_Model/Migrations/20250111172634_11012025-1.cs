using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _110120251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "IDType",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AltCode",
                table: "IDType",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("132a6258-f433-4345-9da1-cc9cfd0b64ac"),
                columns: new[] { "AltCode", "Code" },
                values: new object[] { "03", "PAS" });

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("3e4d4e92-7932-4310-8cda-39b8bdba8d07"),
                columns: new[] { "AltCode", "Code" },
                values: new object[] { "36", "NIT" });

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("429ff286-4d34-42c6-8e6b-254a8d4fa79e"),
                columns: new[] { "AltCode", "Code" },
                values: new object[] { "37", "OTH" });

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("822ac932-46ab-4588-8f82-fbafb14e27eb"),
                columns: new[] { "AltCode", "Code" },
                values: new object[] { "02", "CR" });

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("acae8706-50e1-4296-aacd-bd1d59b946d1"),
                columns: new[] { "AltCode", "Code" },
                values: new object[] { "13", "DUI" });

            migrationBuilder.InsertData(
                table: "IDType",
                columns: new[] { "Id", "AltCode", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { new Guid("7ae985b6-e461-4a66-b028-badf1f16f9f0"), "", "NRC", "7ae985b6-e461-4a66-b028-badf1f16f9f0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Carnet Residente" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5db939b4-1396-4f9a-8d0a-d35804abe5eb", "a1c512db-e2e7-4bf0-9fc6-a65f6218c0b2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("7ae985b6-e461-4a66-b028-badf1f16f9f0"));

            migrationBuilder.DropColumn(
                name: "AltCode",
                table: "IDType");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "IDType",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("132a6258-f433-4345-9da1-cc9cfd0b64ac"),
                column: "Code",
                value: "03");

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("3e4d4e92-7932-4310-8cda-39b8bdba8d07"),
                column: "Code",
                value: "36");

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("429ff286-4d34-42c6-8e6b-254a8d4fa79e"),
                column: "Code",
                value: "37");

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("822ac932-46ab-4588-8f82-fbafb14e27eb"),
                column: "Code",
                value: "02");

            migrationBuilder.UpdateData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("acae8706-50e1-4296-aacd-bd1d59b946d1"),
                column: "Code",
                value: "13");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f59a5dfd-be11-4268-882b-15bab2e91d2f", "801df8a5-82f7-47d8-a54a-dc23e5ee4b27" });
        }
    }
}
