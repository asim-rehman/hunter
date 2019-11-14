using Microsoft.EntityFrameworkCore.Migrations;

namespace Hunter.DataBase.Migrations
{
    public partial class removed_columns_from_devices_and_disabled_lazy_loading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalCount",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MACAddress",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UUID",
                table: "Devices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervalCount",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MACAddress",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UUID",
                table: "Devices",
                nullable: true);
        }
    }
}
