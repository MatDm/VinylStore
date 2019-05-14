using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylStore.Migrations
{
    public partial class VinylForSaleWantlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVinyls");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    VinylId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wantlists",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    VinylId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wantlists", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Wantlists");

            migrationBuilder.CreateTable(
                name: "UserVinyls",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsPossessed = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    VinylId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVinyls", x => x.Id);
                });
        }
    }
}
