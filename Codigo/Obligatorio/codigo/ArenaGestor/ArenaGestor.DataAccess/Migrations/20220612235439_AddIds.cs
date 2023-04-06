using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class AddIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_User_UserId",
                table: "Artist");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                table: "ArtistBand");

            migrationBuilder.DropForeignKey(
                name: "FK_Concert_Location_LocationId",
                table: "Concert");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Countrys_CountryId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Soloist_RoleArtist_RoleArtistId",
                table: "Soloist");

            migrationBuilder.AlterColumn<int>(
                name: "RoleArtistId",
                table: "Soloist",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Concert",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleArtistId",
                table: "ArtistBand",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Artist",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_User_UserId",
                table: "Artist",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                table: "ArtistBand",
                column: "RoleArtistId",
                principalTable: "RoleArtist",
                principalColumn: "RoleArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Concert_Location_LocationId",
                table: "Concert",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Countrys_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Soloist_RoleArtist_RoleArtistId",
                table: "Soloist",
                column: "RoleArtistId",
                principalTable: "RoleArtist",
                principalColumn: "RoleArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_User_UserId",
                table: "Artist");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                table: "ArtistBand");

            migrationBuilder.DropForeignKey(
                name: "FK_Concert_Location_LocationId",
                table: "Concert");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Countrys_CountryId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Soloist_RoleArtist_RoleArtistId",
                table: "Soloist");

            migrationBuilder.AlterColumn<int>(
                name: "RoleArtistId",
                table: "Soloist",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Location",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Concert",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoleArtistId",
                table: "ArtistBand",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Artist",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_User_UserId",
                table: "Artist",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                table: "ArtistBand",
                column: "RoleArtistId",
                principalTable: "RoleArtist",
                principalColumn: "RoleArtistId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Concert_Location_LocationId",
                table: "Concert",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Countrys_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Countrys",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Soloist_RoleArtist_RoleArtistId",
                table: "Soloist",
                column: "RoleArtistId",
                principalTable: "RoleArtist",
                principalColumn: "RoleArtistId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
