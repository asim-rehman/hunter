using Microsoft.EntityFrameworkCore.Migrations;

namespace Hunter.DataBase.Migrations
{
    public partial class removed_status_message_from_tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "Tasks",
                nullable: true);
        }
    }
}
