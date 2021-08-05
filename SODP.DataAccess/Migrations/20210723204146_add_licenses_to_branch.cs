using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class add_licenses_to_branch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licences_Branches_BranchId",
                table: "Licenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_License",
                table: "Licenses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_License",
                table: "Licenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Branches_BranchId",
                table: "Licenses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
