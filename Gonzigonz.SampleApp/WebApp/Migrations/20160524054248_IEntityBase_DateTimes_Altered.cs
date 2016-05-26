using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
	public partial class IEntityBase_DateTimes_Altered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.RenameColumn(
				name: "CreatedTime",
				table: "TodoItems",
				newName: "CreatedTimeUTC");

			migrationBuilder.RenameColumn(
				name: "ModifiedTime",
				table: "TodoItems",
				newName: "ModifiedTimeUTC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.RenameColumn(
				name: "CreatedTimeUTC",
				table: "TodoItems",
				newName: "CreatedTime");

			migrationBuilder.RenameColumn(
				name: "ModifiedTimeUTC",
				table: "TodoItems",
				newName: "ModifiedTime");
		}
    }
}
