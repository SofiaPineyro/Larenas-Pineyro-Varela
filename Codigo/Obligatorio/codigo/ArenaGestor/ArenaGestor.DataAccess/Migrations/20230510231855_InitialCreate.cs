using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countrys",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countrys", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

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

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.TicketStatusId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_Countrys_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countrys",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicalProtagonist",
                columns: table => new
                {
                    MusicalProtagonistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalProtagonist", x => x.MusicalProtagonistId);
                    table.ForeignKey(
                        name: "FK_MusicalProtagonist_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                    table.ForeignKey(
                        name: "FK_Artist_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Session_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.ConcertId);
                    table.ForeignKey(
                        name: "FK_Concert_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Band",
                columns: table => new
                {
                    MusicalProtagonistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Band", x => x.MusicalProtagonistId);
                    table.ForeignKey(
                        name: "FK_Band_MusicalProtagonist_MusicalProtagonistId",
                        column: x => x.MusicalProtagonistId,
                        principalTable: "MusicalProtagonist",
                        principalColumn: "MusicalProtagonistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Soloist",
                columns: table => new
                {
                    MusicalProtagonistId = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    RoleArtistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soloist", x => x.MusicalProtagonistId);
                    table.ForeignKey(
                        name: "FK_Soloist_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Soloist_MusicalProtagonist_MusicalProtagonistId",
                        column: x => x.MusicalProtagonistId,
                        principalTable: "MusicalProtagonist",
                        principalColumn: "MusicalProtagonistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Soloist_RoleArtist_RoleArtistId",
                        column: x => x.RoleArtistId,
                        principalTable: "RoleArtist",
                        principalColumn: "RoleArtistId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Concert_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concert",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketStatus",
                        principalColumn: "TicketStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistBand",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    MusicalProtagonistId = table.Column<int>(type: "int", nullable: false),
                    RoleArtistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistBand", x => new { x.ArtistId, x.MusicalProtagonistId });
                    table.ForeignKey(
                        name: "FK_ArtistBand_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistBand_Band_MusicalProtagonistId",
                        column: x => x.MusicalProtagonistId,
                        principalTable: "Band",
                        principalColumn: "MusicalProtagonistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistBand_RoleArtist_RoleArtistId",
                        column: x => x.RoleArtistId,
                        principalTable: "RoleArtist",
                        principalColumn: "RoleArtistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countrys",
                columns: new[] { "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, "Uruguay" },
                    { 2, "Argentina" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Vendedor" },
                    { 3, "Acomodador" },
                    { 4, "Espectador" },
                    { 5, "Artista" }
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

            migrationBuilder.InsertData(
                table: "TicketStatus",
                columns: new[] { "TicketStatusId", "Status" },
                values: new object[,]
                {
                    { 1, "Comprado" },
                    { 2, "Utilizado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artist_Name",
                table: "Artist",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artist_UserId",
                table: "Artist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBand_MusicalProtagonistId",
                table: "ArtistBand",
                column: "MusicalProtagonistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBand_RoleArtistId",
                table: "ArtistBand",
                column: "RoleArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_LocationId",
                table: "Concert",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertProtagonist_MusicalProtagonistId",
                table: "ConcertProtagonist",
                column: "MusicalProtagonistId");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                table: "Gender",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_CountryId",
                table: "Location",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicalProtagonist_GenderId",
                table: "MusicalProtagonist",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicalProtagonist_Name",
                table: "MusicalProtagonist",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_UserId",
                table: "Session",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Soloist_ArtistId",
                table: "Soloist",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Soloist_RoleArtistId",
                table: "Soloist",
                column: "RoleArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ConcertId",
                table: "Ticket",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketStatusId",
                table: "Ticket",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatus_Status",
                table: "TicketStatus",
                column: "Status",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistBand");

            migrationBuilder.DropTable(
                name: "ConcertProtagonist");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Soloist");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Band");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "RoleArtist");

            migrationBuilder.DropTable(
                name: "Concert");

            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.DropTable(
                name: "MusicalProtagonist");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Countrys");
        }
    }
}
