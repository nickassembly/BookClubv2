using Microsoft.EntityFrameworkCore.Migrations;

namespace BookClub.Migrations
{
    public partial class UserAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAuthorId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAuthors",
                columns: table => new
                {
                    UserAuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuthors", x => x.UserAuthorId);
                    table.ForeignKey(
                        name: "FK_UserAuthors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserAuthorId",
                table: "Authors",
                column: "UserAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuthors_UserId",
                table: "UserAuthors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_UserAuthors_UserAuthorId",
                table: "Authors",
                column: "UserAuthorId",
                principalTable: "UserAuthors",
                principalColumn: "UserAuthorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_UserAuthors_UserAuthorId",
                table: "Authors");

            migrationBuilder.DropTable(
                name: "UserAuthors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserAuthorId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserAuthorId",
                table: "Authors");
        }
    }
}
