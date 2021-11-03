using Microsoft.EntityFrameworkCore.Migrations;

namespace MilkUp.Data.Migrations
{
    public partial class Add_CompanyID_to_Cow_Farm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Cows",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Cows_CompanyID",
                table: "Cows",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cows_Companies_CompanyID",
                table: "Cows",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cows_Companies_CompanyID",
                table: "Cows");

            migrationBuilder.DropIndex(
                name: "IX_Cows_CompanyID",
                table: "Cows");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Cows");
        }
    }
}
