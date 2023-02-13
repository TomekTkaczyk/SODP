using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ModifyParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParts_Parts_PartId",
                table: "ProjectParts");

            migrationBuilder.DropIndex(
                name: "ProjectPartsIX_Part",
                table: "ProjectParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchLicenses",
                table: "BranchLicenses");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "ProjectParts");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProjectParts",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sign",
                table: "ProjectParts",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "PartBranches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BranchRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchLicenses",
                table: "BranchLicenses",
                columns: new[] { "LicenseId", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_PartBranches_BranchId",
                table: "PartBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchRoles_PartBranchId",
                table: "BranchRoles",
                column: "PartBranchId");

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

            migrationBuilder.DropIndex(
                name: "IX_BranchRoles_PartBranchId",
                table: "BranchRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchLicenses",
                table: "BranchLicenses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectParts");

            migrationBuilder.DropColumn(
                name: "Sign",
                table: "ProjectParts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "PartBranches");

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "ProjectParts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BranchRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchLicenses",
                table: "BranchLicenses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "ProjectPartsIX_Part",
                table: "ProjectParts",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParts_Parts_PartId",
                table: "ProjectParts",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
