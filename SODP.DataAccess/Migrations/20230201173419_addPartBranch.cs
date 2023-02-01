using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class addPartBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "PartBranches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PartBranches_BranchId",
                table: "PartBranches",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartBranches_Branches_BranchId",
                table: "PartBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartBranches_Branches_BranchId",
                table: "PartBranches");

            migrationBuilder.DropIndex(
                name: "IX_PartBranches_BranchId",
                table: "PartBranches");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "PartBranches");
        }
    }
}
