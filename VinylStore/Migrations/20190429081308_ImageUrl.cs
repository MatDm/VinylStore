using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylStore.Migrations
{
    public partial class ImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Vinyls");

            migrationBuilder.RenameColumn(
                name: "BandName",
                table: "Vinyls",
                newName: "ReleaseYear");

            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Vinyls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Vinyls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Vinyls");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Vinyls");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "Vinyls",
                newName: "BandName");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Vinyls",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
