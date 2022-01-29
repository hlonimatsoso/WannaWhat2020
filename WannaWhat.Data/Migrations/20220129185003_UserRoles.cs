using Microsoft.EntityFrameworkCore.Migrations;

namespace WannaWhat.Data.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WannaWhatUserId",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_WannaWhatUserId",
                table: "AspNetRoles",
                column: "WannaWhatUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_WannaWhatUserId",
                table: "AspNetRoles",
                column: "WannaWhatUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_WannaWhatUserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_WannaWhatUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "WannaWhatUserId",
                table: "AspNetRoles");
        }
    }
}
