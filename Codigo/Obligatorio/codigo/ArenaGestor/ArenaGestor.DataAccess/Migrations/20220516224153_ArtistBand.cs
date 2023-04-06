using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class ArtistBand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_Artist_ArtistsArtistId",
                table: "ArtistBand");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_Band_BandsMusicalProtagonistId",
                table: "ArtistBand");

            migrationBuilder.RenameColumn(
                name: "BandsMusicalProtagonistId",
                table: "ArtistBand",
                newName: "MusicalProtagonistId");

            migrationBuilder.RenameColumn(
                name: "ArtistsArtistId",
                table: "ArtistBand",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistBand_BandsMusicalProtagonistId",
                table: "ArtistBand",
                newName: "IX_ArtistBand_MusicalProtagonistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_Artist_ArtistId",
                table: "ArtistBand",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_Band_MusicalProtagonistId",
                table: "ArtistBand",
                column: "MusicalProtagonistId",
                principalTable: "Band",
                principalColumn: "MusicalProtagonistId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_Artist_ArtistId",
                table: "ArtistBand");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_Band_MusicalProtagonistId",
                table: "ArtistBand");

            migrationBuilder.RenameColumn(
                name: "MusicalProtagonistId",
                table: "ArtistBand",
                newName: "BandsMusicalProtagonistId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ArtistBand",
                newName: "ArtistsArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistBand_MusicalProtagonistId",
                table: "ArtistBand",
                newName: "IX_ArtistBand_BandsMusicalProtagonistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_Artist_ArtistsArtistId",
                table: "ArtistBand",
                column: "ArtistsArtistId",
                principalTable: "Artist",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_Band_BandsMusicalProtagonistId",
                table: "ArtistBand",
                column: "BandsMusicalProtagonistId",
                principalTable: "Band",
                principalColumn: "MusicalProtagonistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
