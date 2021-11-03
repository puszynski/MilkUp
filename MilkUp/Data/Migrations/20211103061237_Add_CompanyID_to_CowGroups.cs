using Microsoft.EntityFrameworkCore.Migrations;

namespace MilkUp.Data.Migrations
{
    public partial class Add_CompanyID_to_CowGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "CowGroups",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_CowGroups_CompanyID",
                table: "CowGroups",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_CowGroups_Companies_CompanyID",
                table: "CowGroups",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CowGroups_Companies_CompanyID",
                table: "CowGroups");

            migrationBuilder.DropIndex(
                name: "IX_CowGroups_CompanyID",
                table: "CowGroups");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "CowGroups");
        }
    }
}
