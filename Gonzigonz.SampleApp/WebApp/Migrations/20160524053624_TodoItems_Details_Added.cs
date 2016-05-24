using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
	public partial class TodoItems_Details_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "TodoItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "TodoItems");
        }
    }
}
