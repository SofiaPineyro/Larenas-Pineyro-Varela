using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class county : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countrys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countrys",
                table: "Countrys",
                column: "CountryId");

            migrationBuilder.InsertData(
                table: "Countrys",
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Uruguay" });

            migrationBuilder.InsertData(
                table: "Countrys",
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "Argentina" });

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Countrys_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Countrys_CountryId",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countrys",
                table: "Countrys");

            migrationBuilder.DeleteData(
                table: "Countrys",
                keyColumn: "CountryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countrys",
                keyColumn: "CountryId",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Countrys",
                newName: "Country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
