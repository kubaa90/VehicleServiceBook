using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleServiceBook.Migrations
{
    public partial class FaultUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDateTimeString",
                table: "Faults");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDateTime",
                table: "Faults",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Faults",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnalyzeDateTime",
                table: "Faults",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDateTime",
                table: "Faults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Faults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "AnalyzeDateTime",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "ClosedDateTime",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Faults");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddDateTime",
                table: "Faults",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "AddDateTimeString",
                table: "Faults",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
