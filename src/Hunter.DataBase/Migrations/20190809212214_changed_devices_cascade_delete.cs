using Microsoft.EntityFrameworkCore.Migrations;

namespace Hunter.DataBase.Migrations
{
    public partial class changed_devices_cascade_delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_User_UserId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_User_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_User_UserId",
                table: "Devices");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_User_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
