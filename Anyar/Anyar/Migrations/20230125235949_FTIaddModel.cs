using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anyar.Migrations
{
    public partial class FTIaddModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Employees");
        }
    }
}
