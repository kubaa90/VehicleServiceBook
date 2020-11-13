using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleServiceBook.Migrations
{
    public partial class FaultUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Faults",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_AspNetUsers_UserId",
                table: "Faults");

            migrationBuilder.DropIndex(
                name: "IX_Faults_UserId",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Faults");
        }
    }
}
