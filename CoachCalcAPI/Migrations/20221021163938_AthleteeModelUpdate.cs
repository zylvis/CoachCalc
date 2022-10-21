using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachCalcAPI.Migrations
{
    public partial class AthleteeModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Athletees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Athletees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Athletees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Athletees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Athletees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Athletees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Athletees");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Athletees");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Athletees");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Athletees");
        }
    }
}
