using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class PluraliseLookupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UrlLookup",
                table: "UrlLookup");

            migrationBuilder.RenameTable(
                name: "UrlLookup",
                newName: "UrlLookups");

            migrationBuilder.RenameIndex(
                name: "IX_UrlLookup_Url",
                table: "UrlLookups",
                newName: "IX_UrlLookups_Url");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrlLookups",
                table: "UrlLookups",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UrlLookups",
                table: "UrlLookups");

            migrationBuilder.RenameTable(
                name: "UrlLookups",
                newName: "UrlLookup");

            migrationBuilder.RenameIndex(
                name: "IX_UrlLookups_Url",
                table: "UrlLookup",
                newName: "IX_UrlLookup_Url");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrlLookup",
                table: "UrlLookup",
                column: "Key");
        }
    }
}
