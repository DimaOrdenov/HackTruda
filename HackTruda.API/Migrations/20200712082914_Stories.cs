using Microsoft.EntityFrameworkCore.Migrations;

namespace HackTruda.API.Migrations
{
    public partial class Stories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStory",
                table: "Post",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStory",
                table: "Post");
        }
    }
}
