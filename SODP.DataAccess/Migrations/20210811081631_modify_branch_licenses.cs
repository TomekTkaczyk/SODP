using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class modify_branch_licenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Branches_BranchId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranch_Branches_BranchId",
                table: "ProjectBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranch_Licenses_CheckingLicenseId",
                table: "ProjectBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranch_Licenses_DesignerLicenseId",
                table: "ProjectBranch");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_BranchId",
                table: "Licenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchLicense",
                table: "BranchLicense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectBranch",
                table: "ProjectBranch");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Licenses");

            migrationBuilder.RenameTable(
                name: "ProjectBranch",
                newName: "ProjectBranches");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBranch_ProjectId",
                table: "ProjectBranches",
                newName: "IX_Project");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBranch_DesignerLicenseId",
                table: "ProjectBranches",
                newName: "IX_Designer");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBranch_CheckingLicenseId",
                table: "ProjectBranches",
                newName: "IX_Checking");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectBranch_BranchId",
                table: "ProjectBranches",
                newName: "IX_Branch");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BranchLicense",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchLicense",
                table: "BranchLicense",
                columns: new[] { "BranchId", "LicenseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectBranches",
                table: "ProjectBranches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Branches_BranchId",
                table: "ProjectBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Licenses_CheckingLicenseId",
                table: "ProjectBranches",
                column: "CheckingLicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Licenses_DesignerLicenseId",
                table: "ProjectBranches",
                column: "DesignerLicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Branches_BranchId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licenses_CheckingLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licenses_DesignerLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchLicense",
                table: "BranchLicense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectBranches",
                table: "ProjectBranches");

            migrationBuilder.RenameTable(
                name: "ProjectBranches",
                newName: "ProjectBranch");

            migrationBuilder.RenameIndex(
                name: "IX_Project",
                table: "ProjectBranch",
                newName: "IX_ProjectBranch_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Designer",
                table: "ProjectBranch",
                newName: "IX_ProjectBranch_DesignerLicenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Checking",
                table: "ProjectBranch",
                newName: "IX_ProjectBranch_CheckingLicenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Branch",
                table: "ProjectBranch",
                newName: "IX_ProjectBranch_BranchId");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Licenses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BranchLicense",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchLicense",
                table: "BranchLicense",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectBranch",
                table: "ProjectBranch",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_BranchId",
                table: "Licenses",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Branches_BranchId",
                table: "Licenses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranch_Branches_BranchId",
                table: "ProjectBranch",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranch_Licenses_CheckingLicenseId",
                table: "ProjectBranch",
                column: "CheckingLicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranch_Licenses_DesignerLicenseId",
                table: "ProjectBranch",
                column: "DesignerLicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
