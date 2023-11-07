using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ModifyBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Stages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Stages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ProjectParts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "ProjectParts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Parts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Parts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "PartBranches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "PartBranches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Licenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Licenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Investors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Investors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Dictionary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Dictionary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Designers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Designers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Certificates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Certificates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "BranchRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "BranchRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "BranchLicenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "BranchLicenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Branches",
                nullable: false,
                defaultValue: 0);

			migrationBuilder.Sql("UPDATE Branches SET CreatedById = 1");

			migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Branches",
                nullable: false,
                defaultValue: 0);

			migrationBuilder.Sql("UPDATE Branches SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Tokens_CreatedById",
                table: "Tokens",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Tokens SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Tokens_ModifiedById",
                table: "Tokens",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Tokens SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Stages_CreatedById",
                table: "Stages",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Stages SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Stages_ModifiedById",
                table: "Stages",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Stages SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Projects SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Projects_ModifiedById",
                table: "Projects",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Projects SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_ProjectParts_CreatedById",
                table: "ProjectParts",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE ProjectParts SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_ProjectParts_ModifiedById",
                table: "ProjectParts",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE ProjectParts SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Parts_CreatedById",
                table: "Parts",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Parts SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Parts_ModifiedById",
                table: "Parts",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Parts SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_PartBranches_CreatedById",
                table: "PartBranches",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE PartBranches SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_PartBranches_ModifiedById",
                table: "PartBranches",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE PartBranches SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Licenses_CreatedById",
                table: "Licenses",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Licenses SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Licenses_ModifiedById",
                table: "Licenses",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Licenses SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Investors_CreatedById",
                table: "Investors",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Investors SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Investors_ModifiedById",
                table: "Investors",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Investors SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Dictionary_CreatedById",
                table: "Dictionary",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Dictionary SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Dictionary_ModifiedById",
                table: "Dictionary",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Dictionary SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Designers_CreatedById",
                table: "Designers",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Designers SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Designers_ModifiedById",
                table: "Designers",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Designers SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Certificates_CreatedById",
                table: "Certificates",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Certificates SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Certificates_ModifiedById",
                table: "Certificates",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Certificates SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_BranchRoles_CreatedById",
                table: "BranchRoles",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE BranchRoles SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_BranchRoles_ModifiedById",
                table: "BranchRoles",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE BranchRoles SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_BranchLicenses_CreatedById",
                table: "BranchLicenses",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE BranchLicenses SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_BranchLicenses_ModifiedById",
                table: "BranchLicenses",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE BranchLicenses SET ModifiedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Branches_CreatedById",
                table: "Branches",
                column: "CreatedById");

			migrationBuilder.Sql("UPDATE Branches SET CreatedById = 1");

			migrationBuilder.CreateIndex(
                name: "IX_Branches_ModifiedById",
                table: "Branches",
                column: "ModifiedById");

			migrationBuilder.Sql("UPDATE Branches SET ModifiedById = 1");

			migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_CreatedById",
                table: "Branches",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_ModifiedById",
                table: "Branches",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchLicenses_Users_CreatedById",
                table: "BranchLicenses",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchLicenses_Users_ModifiedById",
                table: "BranchLicenses",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchRoles_Users_CreatedById",
                table: "BranchRoles",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchRoles_Users_ModifiedById",
                table: "BranchRoles",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Users_CreatedById",
                table: "Certificates",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Users_ModifiedById",
                table: "Certificates",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designers_Users_CreatedById",
                table: "Designers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Designers_Users_ModifiedById",
                table: "Designers",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionary_Users_CreatedById",
                table: "Dictionary",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dictionary_Users_ModifiedById",
                table: "Dictionary",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Investors_Users_CreatedById",
                table: "Investors",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Investors_Users_ModifiedById",
                table: "Investors",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Users_CreatedById",
                table: "Licenses",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Users_ModifiedById",
                table: "Licenses",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartBranches_Users_CreatedById",
                table: "PartBranches",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartBranches_Users_ModifiedById",
                table: "PartBranches",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Users_CreatedById",
                table: "Parts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Users_ModifiedById",
                table: "Parts",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParts_Users_CreatedById",
                table: "ProjectParts",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParts_Users_ModifiedById",
                table: "ProjectParts",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatedById",
                table: "Projects",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_ModifiedById",
                table: "Projects",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Users_CreatedById",
                table: "Stages",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Users_ModifiedById",
                table: "Stages",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_CreatedById",
                table: "Tokens",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_ModifiedById",
                table: "Tokens",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_CreatedById",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_ModifiedById",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchLicenses_Users_CreatedById",
                table: "BranchLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchLicenses_Users_ModifiedById",
                table: "BranchLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchRoles_Users_CreatedById",
                table: "BranchRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchRoles_Users_ModifiedById",
                table: "BranchRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Users_CreatedById",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Users_ModifiedById",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Designers_Users_CreatedById",
                table: "Designers");

            migrationBuilder.DropForeignKey(
                name: "FK_Designers_Users_ModifiedById",
                table: "Designers");

            migrationBuilder.DropForeignKey(
                name: "FK_Dictionary_Users_CreatedById",
                table: "Dictionary");

            migrationBuilder.DropForeignKey(
                name: "FK_Dictionary_Users_ModifiedById",
                table: "Dictionary");

            migrationBuilder.DropForeignKey(
                name: "FK_Investors_Users_CreatedById",
                table: "Investors");

            migrationBuilder.DropForeignKey(
                name: "FK_Investors_Users_ModifiedById",
                table: "Investors");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Users_CreatedById",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Users_ModifiedById",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PartBranches_Users_CreatedById",
                table: "PartBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PartBranches_Users_ModifiedById",
                table: "PartBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Users_CreatedById",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Users_ModifiedById",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParts_Users_CreatedById",
                table: "ProjectParts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParts_Users_ModifiedById",
                table: "ProjectParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_ModifiedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Users_CreatedById",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Users_ModifiedById",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_CreatedById",
                table: "Tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_ModifiedById",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_CreatedById",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ModifiedById",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Stages_CreatedById",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_ModifiedById",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ModifiedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectParts_CreatedById",
                table: "ProjectParts");

            migrationBuilder.DropIndex(
                name: "IX_ProjectParts_ModifiedById",
                table: "ProjectParts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_CreatedById",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ModifiedById",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_PartBranches_CreatedById",
                table: "PartBranches");

            migrationBuilder.DropIndex(
                name: "IX_PartBranches_ModifiedById",
                table: "PartBranches");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_CreatedById",
                table: "Licenses");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_ModifiedById",
                table: "Licenses");

            migrationBuilder.DropIndex(
                name: "IX_Investors_CreatedById",
                table: "Investors");

            migrationBuilder.DropIndex(
                name: "IX_Investors_ModifiedById",
                table: "Investors");

            migrationBuilder.DropIndex(
                name: "IX_Dictionary_CreatedById",
                table: "Dictionary");

            migrationBuilder.DropIndex(
                name: "IX_Dictionary_ModifiedById",
                table: "Dictionary");

            migrationBuilder.DropIndex(
                name: "IX_Designers_CreatedById",
                table: "Designers");

            migrationBuilder.DropIndex(
                name: "IX_Designers_ModifiedById",
                table: "Designers");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_CreatedById",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ModifiedById",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_BranchRoles_CreatedById",
                table: "BranchRoles");

            migrationBuilder.DropIndex(
                name: "IX_BranchRoles_ModifiedById",
                table: "BranchRoles");

            migrationBuilder.DropIndex(
                name: "IX_BranchLicenses_CreatedById",
                table: "BranchLicenses");

            migrationBuilder.DropIndex(
                name: "IX_BranchLicenses_ModifiedById",
                table: "BranchLicenses");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CreatedById",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ModifiedById",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProjectParts");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "ProjectParts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PartBranches");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "PartBranches");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Investors");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Investors");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Dictionary");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Dictionary");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "BranchRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "BranchRoles");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "BranchLicenses");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "BranchLicenses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Branches");
        }
    }
}
