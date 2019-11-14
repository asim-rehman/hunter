using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hunter.DataBase.Migrations
{
    public partial class removed_devicetypes_IMEI_lastconnected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMEI",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LastConnected",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Devices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMEI",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastConnected",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Devices",
                nullable: false,
                defaultValue: 0);
        }
    }
}
