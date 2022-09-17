using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachCalcAPI.Migrations
{
    public partial class AddImageToAthlette : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
  
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Athletees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "SearchColumn",
                table: "Athletees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
