using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleServiceBook.Migrations
{
    public partial class ProcessFault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_AspNetUsers_UserId",
                table: "Faults");

            migrationBuilder.DropIndex(
                name: "IX_Faults_UserId",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "AnalyzeDateTime",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "ClosedDateTime",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Faults");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDateTime",
                table: "Faults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateUserName",
                table: "Faults",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessDateTime",
                table: "Faults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessedUserName",
                table: "Faults",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDateTime",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "CreateUserName",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "ProcessDateTime",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "ProcessedUserName",
                table: "Faults");

            migrationBuilder.AddColumn<DateTime>(
                name: "AnalyzeDateTime",
                table: "Faults",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDateTime",
                table: "Faults",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Faults",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faults_UserId",
                table: "Faults",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_AspNetUsers_UserId",
                table: "Faults",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
