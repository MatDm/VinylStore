using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylStore.Migrations
{
    public partial class labeldescriptionspotifyalbumid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vinyls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Vinyls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpotifyAlbumId",
                table: "Vinyls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vinyls");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Vinyls");

            migrationBuilder.DropColumn(
                name: "SpotifyAlbumId",
                table: "Vinyls");
        }
    }
}
