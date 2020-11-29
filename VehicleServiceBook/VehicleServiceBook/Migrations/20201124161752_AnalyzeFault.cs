using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleServiceBook.Migrations
{
    public partial class AnalyzeFault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasFault",
                table: "Vehicles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAbleToDrive",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatorRemarks",
                table: "Faults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFault",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsAbleToDrive",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "OperatorRemarks",
                table: "Faults");
        }
    }
}
