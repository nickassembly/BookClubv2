using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookClub.Migrations
{
    public partial class ChangedAuthorBioTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BiographyDescription",
                table: "AuthorBios");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AuthorBios");

            migrationBuilder.RenameColumn(
                name: "PlaceOfBirth",
                table: "AuthorBios",
                newName: "BiographyNotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BiographyNotes",
                table: "AuthorBios",
                newName: "PlaceOfBirth");

            migrationBuilder.AddColumn<string>(
                name: "BiographyDescription",
                table: "AuthorBios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AuthorBios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
