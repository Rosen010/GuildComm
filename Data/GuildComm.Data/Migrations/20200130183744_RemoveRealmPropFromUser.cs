using Microsoft.EntityFrameworkCore.Migrations;

namespace GuildComm.Data.Migrations
{
    public partial class RemoveRealmPropFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Realms_RealmId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RealmId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealmId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RealmId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RealmId",
                table: "AspNetUsers",
                column: "RealmId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Realms_RealmId",
                table: "AspNetUsers",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
