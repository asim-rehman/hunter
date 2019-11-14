using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Hunter.DataBase.Migrations
{
    public partial class add_user_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskData_Tasks_TasksId",
                table: "TaskData");

            migrationBuilder.AlterColumn<Guid>(
                name: "TasksId",
                table: "TaskData",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_User_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskData_Tasks_TasksId",
                table: "TaskData",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_User_UserId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskData_Tasks_TasksId",
                table: "TaskData");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Devices");

            migrationBuilder.AlterColumn<Guid>(
                name: "TasksId",
                table: "TaskData",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_TaskData_Tasks_TasksId",
                table: "TaskData",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
