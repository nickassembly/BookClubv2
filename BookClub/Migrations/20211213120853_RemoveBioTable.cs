using Microsoft.EntityFrameworkCore.Migrations;

namespace BookClub.Migrations
{
    public partial class RemoveBioTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBios");

            migrationBuilder.DropColumn(
                name: "AuthorBioId",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "BiographyNotes",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BiographyNotes",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "AuthorBioId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuthorBios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BiographyNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorBios_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBios_AuthorId",
                table: "AuthorBios",
                column: "AuthorId",
                unique: true);
        }
    }
}
