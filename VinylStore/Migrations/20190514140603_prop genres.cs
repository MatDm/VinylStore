using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylStore.Migrations
{
    public partial class propgenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Vinyls",
                newName: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genres",
                table: "Vinyls",
                newName: "Genre");
        }
    }
}
