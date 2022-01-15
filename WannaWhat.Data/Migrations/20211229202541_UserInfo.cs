using Microsoft.EntityFrameworkCore.Migrations;

namespace WannaWhat.Data.Migrations
{
    public partial class UserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "UserInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "UserInfo");
        }
    }
}
