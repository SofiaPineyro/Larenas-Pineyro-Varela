using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class MultipleProtagonistsPerConcert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concert_MusicalProtagonist_MusicalProtagonistId",
                table: "Concert");

            migrationBuilder.DropIndex(
                name: "IX_Concert_MusicalProtagonistId",
                table: "Concert");

            migrationBuilder.DropColumn(
                name: "MusicalProtagonistId",
                table: "Concert");

            migrationBuilder.CreateTable(
                name: "ConcertProtagonist",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    MusicalProtagonistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertProtagonist", x => new { x.ConcertId, x.MusicalProtagonistId });
                    table.ForeignKey(
                        name: "FK_ConcertProtagonist_Concert_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concert",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcertProtagonist_MusicalProtagonist_MusicalProtagonistId",
                        column: x => x.MusicalProtagonistId,
                        principalTable: "MusicalProtagonist",
                        principalColumn: "MusicalProtagonistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcertProtagonist_MusicalProtagonistId",
                table: "ConcertProtagonist",
                column: "MusicalProtagonistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcertProtagonist");

            migrationBuilder.AddColumn<int>(
                name: "MusicalProtagonistId",
                table: "Concert",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Concert_MusicalProtagonistId",
                table: "Concert",
                column: "MusicalProtagonistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Concert_MusicalProtagonist_MusicalProtagonistId",
                table: "Concert",
                column: "MusicalProtagonistId",
                principalTable: "MusicalProtagonist",
                principalColumn: "MusicalProtagonistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
