using Microsoft.EntityFrameworkCore.Migrations;

namespace GuildComm.Data.Migrations
{
    public partial class AddedRealmEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RealmId",
                table: "Guilds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealmId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Realm",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Region = table.Column<int>(nullable: false),
                    RealmType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realm", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_RealmId",
                table: "Guilds",
                column: "RealmId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RealmId",
                table: "AspNetUsers",
                column: "RealmId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Realm_RealmId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Realm_RealmId",
                table: "Guilds");

            migrationBuilder.DropTable(
                name: "Realm");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_RealmId",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RealmId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealmId",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "RealmId",
                table: "AspNetUsers");
        }
    }
}
