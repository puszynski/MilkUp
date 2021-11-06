using Microsoft.EntityFrameworkCore.Migrations;

namespace MilkUp.Data.Migrations
{
    public partial class Lactation_deleteColumn_DaysOfLactationing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfLactationing",
                table: "Lactations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfLactationing",
                table: "Lactations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
