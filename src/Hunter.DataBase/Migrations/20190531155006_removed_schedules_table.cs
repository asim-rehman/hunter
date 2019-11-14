using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Hunter.DataBase.Migrations
{
    public partial class removed_schedules_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntervalDays",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntervalSeconds",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRun",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextRun",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IntervalDays",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IntervalSeconds",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastRun",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "NextRun",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Tasks");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    End = table.Column<DateTime>(nullable: true),
                    IntervalDays = table.Column<int>(nullable: false),
                    IntervalSeconds = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    LastRun = table.Column<DateTime>(nullable: true),
                    NextRun = table.Column<DateTime>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TaskId",
                table: "Schedules",
                column: "TaskId",
                unique: true);
        }
    }
}
