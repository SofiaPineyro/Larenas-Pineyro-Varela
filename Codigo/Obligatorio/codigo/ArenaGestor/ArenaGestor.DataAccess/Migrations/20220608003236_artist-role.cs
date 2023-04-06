using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class artistrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleArtistId",
                table: "Soloist",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleArtistId",
                table: "ArtistBand",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleArtist",
                columns: table => new
                {
                    RoleArtistId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleArtist", x => x.RoleArtistId);
                });

            migrationBuilder.InsertData(
                table: "RoleArtist",
                columns: new[] { "RoleArtistId", "Name" },
                values: new object[,]
                {
                    { 1, "Cantante" },
                    { 2, "Baterista" },
                    { 3, "Bajista" },
                    { 4, "Guitarrista" },
                    { 5, "Coro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Soloist_RoleArtistId",
                table: "Soloist",
                column: "RoleArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBand_RoleArtistId",
                table: "ArtistBand",
                column: "RoleArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                table: "ArtistBand",
                column: "RoleArtistId",
                principalTable: "RoleArtist",
                principalColumn: "RoleArtistId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Soloist_RoleArtist_RoleArtistId",
                table: "Soloist",
                column: "RoleArtistId",
                principalTable: "RoleArtist",
                principalColumn: "RoleArtistId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                table: "ArtistBand");

            migrationBuilder.DropForeignKey(
                name: "FK_Soloist_RoleArtist_RoleArtistId",
                table: "Soloist");

            migrationBuilder.DropTable(
                name: "RoleArtist");

            migrationBuilder.DropIndex(
                name: "IX_Soloist_RoleArtistId",
                table: "Soloist");

            migrationBuilder.DropIndex(
                name: "IX_ArtistBand_RoleArtistId",
                table: "ArtistBand");

            migrationBuilder.DropColumn(
                name: "RoleArtistId",
                table: "Soloist");

            migrationBuilder.DropColumn(
                name: "RoleArtistId",
                table: "ArtistBand");
        }
    }
}
