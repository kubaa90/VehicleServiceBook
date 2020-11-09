using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleServiceBook.Migrations
{
    public partial class VehicleProducerMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegistrationDateString",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDateString",
                table: "Vehicles");
        }
    }
}
