using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatisticsWebApp.Migrations
{
    public partial class ChangeOfStatisticModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LinesCount",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "LinesCount",
                table: "Statistics");
        }
    }
}
