using Microsoft.EntityFrameworkCore.Migrations;

namespace GuildComm.Data.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Realm_RealmId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Realm_RealmId",
                table: "Guilds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Realm",
                table: "Realm");

            migrationBuilder.RenameTable(
                name: "Realm",
                newName: "Realms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Realms",
                table: "Realms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Realms_RealmId",
                table: "AspNetUsers",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Realms_RealmId",
                table: "Guilds",
                column: "RealmId",
                principalTable: "Realms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Realms_RealmId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Realms_RealmId",
                table: "Guilds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Realms",
                table: "Realms");

            migrationBuilder.RenameTable(
                name: "Realms",
                newName: "Realm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Realm",
                table: "Realm",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Realm_RealmId",
                table: "AspNetUsers",
                column: "RealmId",
                principalTable: "Realm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Realm_RealmId",
                table: "Guilds",
                column: "RealmId",
                principalTable: "Realm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
