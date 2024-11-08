using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _071120241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEconomicActivity_Company_CompanyId",
                table: "CompanyEconomicActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEconomicActivity_EconomicActivity_EconomicActivityId",
                table: "CompanyEconomicActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerEconomicActivity_EconomicActivity_EconomicActivityId",
                table: "PartnerEconomicActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EconomicActivity",
                table: "EconomicActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEconomicActivity",
                table: "CompanyEconomicActivity");

            migrationBuilder.RenameTable(
                name: "EconomicActivity",
                newName: "EconomicActivities");

            migrationBuilder.RenameTable(
                name: "CompanyEconomicActivity",
                newName: "CompanyEconomicActivities");

            migrationBuilder.RenameIndex(
                name: "IX_EconomicActivity_Code",
                table: "EconomicActivities",
                newName: "IX_EconomicActivities_Code");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEconomicActivity_EconomicActivityId",
                table: "CompanyEconomicActivities",
                newName: "IX_CompanyEconomicActivities_EconomicActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEconomicActivity_CompanyId",
                table: "CompanyEconomicActivities",
                newName: "IX_CompanyEconomicActivities_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EconomicActivities",
                table: "EconomicActivities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEconomicActivities",
                table: "CompanyEconomicActivities",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IDType",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("132a6258-f433-4345-9da1-cc9cfd0b64ac"), "03", "132a6258-f433-4345-9da1-cc9cfd0b64ac", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pasaporte" },
                    { new Guid("3e4d4e92-7932-4310-8cda-39b8bdba8d07"), "36", "3e4d4e92-7932-4310-8cda-39b8bdba8d07", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NIT" },
                    { new Guid("429ff286-4d34-42c6-8e6b-254a8d4fa79e"), "37", "429ff286-4d34-42c6-8e6b-254a8d4fa79e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Otro" },
                    { new Guid("822ac932-46ab-4588-8f82-fbafb14e27eb"), "02", "822ac932-46ab-4588-8f82-fbafb14e27eb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Carnet Residente" },
                    { new Guid("acae8706-50e1-4296-aacd-bd1d59b946d1"), "13", "acae8706-50e1-4296-aacd-bd1d59b946d1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "DUI" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3a72e61b-3d0c-4b39-9ab1-2f5635df62b8", "cc86f9b3-ddeb-4329-8b0e-68bc5b7d163a" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEconomicActivities_Company_CompanyId",
                table: "CompanyEconomicActivities",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEconomicActivities_EconomicActivities_EconomicActivityId",
                table: "CompanyEconomicActivities",
                column: "EconomicActivityId",
                principalTable: "EconomicActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerEconomicActivity_EconomicActivities_EconomicActivityId",
                table: "PartnerEconomicActivity",
                column: "EconomicActivityId",
                principalTable: "EconomicActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEconomicActivities_Company_CompanyId",
                table: "CompanyEconomicActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEconomicActivities_EconomicActivities_EconomicActivityId",
                table: "CompanyEconomicActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerEconomicActivity_EconomicActivities_EconomicActivityId",
                table: "PartnerEconomicActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EconomicActivities",
                table: "EconomicActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEconomicActivities",
                table: "CompanyEconomicActivities");

            migrationBuilder.DeleteData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("132a6258-f433-4345-9da1-cc9cfd0b64ac"));

            migrationBuilder.DeleteData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("3e4d4e92-7932-4310-8cda-39b8bdba8d07"));

            migrationBuilder.DeleteData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("429ff286-4d34-42c6-8e6b-254a8d4fa79e"));

            migrationBuilder.DeleteData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("822ac932-46ab-4588-8f82-fbafb14e27eb"));

            migrationBuilder.DeleteData(
                table: "IDType",
                keyColumn: "Id",
                keyValue: new Guid("acae8706-50e1-4296-aacd-bd1d59b946d1"));

            migrationBuilder.RenameTable(
                name: "EconomicActivities",
                newName: "EconomicActivity");

            migrationBuilder.RenameTable(
                name: "CompanyEconomicActivities",
                newName: "CompanyEconomicActivity");

            migrationBuilder.RenameIndex(
                name: "IX_EconomicActivities_Code",
                table: "EconomicActivity",
                newName: "IX_EconomicActivity_Code");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEconomicActivities_EconomicActivityId",
                table: "CompanyEconomicActivity",
                newName: "IX_CompanyEconomicActivity_EconomicActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEconomicActivities_CompanyId",
                table: "CompanyEconomicActivity",
                newName: "IX_CompanyEconomicActivity_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EconomicActivity",
                table: "EconomicActivity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEconomicActivity",
                table: "CompanyEconomicActivity",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "48e4f77e-571a-48a5-95fd-c393ab1a8c4a", "7125829b-e026-4c13-bc83-1257e79bde52" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEconomicActivity_Company_CompanyId",
                table: "CompanyEconomicActivity",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEconomicActivity_EconomicActivity_EconomicActivityId",
                table: "CompanyEconomicActivity",
                column: "EconomicActivityId",
                principalTable: "EconomicActivity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerEconomicActivity_EconomicActivity_EconomicActivityId",
                table: "PartnerEconomicActivity",
                column: "EconomicActivityId",
                principalTable: "EconomicActivity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
