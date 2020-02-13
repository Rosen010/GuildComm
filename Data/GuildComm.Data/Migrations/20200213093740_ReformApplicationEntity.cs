using Microsoft.EntityFrameworkCore.Migrations;

namespace GuildComm.Data.Migrations
{
    public partial class ReformApplicationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainCharacterName",
                table: "Application");

            migrationBuilder.AddColumn<string>(
                name: "ArmoryLink",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharacterName",
                table: "Application",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmoryLink",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "CharacterName",
                table: "Application");

            migrationBuilder.AddColumn<string>(
                name: "MainCharacterName",
                table: "Application",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
