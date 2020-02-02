using Microsoft.EntityFrameworkCore.Migrations;

namespace GuildComm.Data.Migrations
{
    public partial class AddGuildPropToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInGuild",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "GuildId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GuildId",
                table: "AspNetUsers",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Guilds_GuildId",
                table: "AspNetUsers",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Guilds_GuildId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GuildId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsInGuild",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
