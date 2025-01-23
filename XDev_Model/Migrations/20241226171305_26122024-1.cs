using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XDev_Model.Migrations
{
    /// <inheritdoc />
    public partial class _261220241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceSchemeCondition_PriceScheme_PriceSchemaId",
                table: "PriceSchemeCondition");

            migrationBuilder.RenameColumn(
                name: "PriceSchemaId",
                table: "PriceSchemeCondition",
                newName: "PriceSchemeId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceSchemeCondition_PriceSchemaId",
                table: "PriceSchemeCondition",
                newName: "IX_PriceSchemeCondition_PriceSchemeId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fc820faa-b31e-4a4b-9823-dc15321d422d", "20f1aaba-52d1-4c93-8a92-19bf5ba289cb" });

            migrationBuilder.AddForeignKey(
                name: "FK_PriceSchemeCondition_PriceScheme_PriceSchemeId",
                table: "PriceSchemeCondition",
                column: "PriceSchemeId",
                principalTable: "PriceScheme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceSchemeCondition_PriceScheme_PriceSchemeId",
                table: "PriceSchemeCondition");

            migrationBuilder.RenameColumn(
                name: "PriceSchemeId",
                table: "PriceSchemeCondition",
                newName: "PriceSchemaId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceSchemeCondition_PriceSchemeId",
                table: "PriceSchemeCondition",
                newName: "IX_PriceSchemeCondition_PriceSchemaId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8c5b29e5-fbb8-4cc9-871a-d61aaf739bf5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "364540e5-7d04-4d94-84d1-08d215fd2ace", "99847d14-b755-4ad8-a340-fe5883dcdfea" });

            migrationBuilder.AddForeignKey(
                name: "FK_PriceSchemeCondition_PriceScheme_PriceSchemaId",
                table: "PriceSchemeCondition",
                column: "PriceSchemaId",
                principalTable: "PriceScheme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
