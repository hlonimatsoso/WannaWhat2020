using Microsoft.EntityFrameworkCore.Migrations;

namespace WannaWhat.Data.Migrations
{
    public partial class UserPersonality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personality",
                columns: table => new
                {
                    PersonalityId = table.Column<string>(nullable: false),
                    PersonalityName = table.Column<string>(type: "varchar(20)", nullable: true),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personality", x => x.PersonalityId);
                });

            migrationBuilder.CreateTable(
                name: "UserPersonality",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    PersonalityId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPersonality", x => new { x.UserId, x.PersonalityId });
                    table.ForeignKey(
                        name: "FK_UserPersonality_Personality_PersonalityId",
                        column: x => x.PersonalityId,
                        principalTable: "Personality",
                        principalColumn: "PersonalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPersonality_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPersonality_PersonalityId",
                table: "UserPersonality",
                column: "PersonalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPersonality");

            migrationBuilder.DropTable(
                name: "Personality");
        }
    }
}
