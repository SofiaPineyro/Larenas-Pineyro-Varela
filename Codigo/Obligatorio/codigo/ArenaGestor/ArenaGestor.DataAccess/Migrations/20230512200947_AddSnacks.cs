using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class AddSnacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Snack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snack", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Snack_Name",
                table: "Snack",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Snack");
        }
    }
}
