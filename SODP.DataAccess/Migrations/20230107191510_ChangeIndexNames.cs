using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ChangeIndexNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranch",
                table: "ProjectBranchRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserName",
                table: "Users",
                newName: "UsersIX_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_MormalizedUserName",
                table: "Users",
                newName: "UsersIX_MormalizedUserName");

            migrationBuilder.RenameIndex(
                name: "IX_NormalizedEmail",
                table: "Users",
                newName: "UsersIX_NormalizedEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Email",
                table: "Users",
                newName: "UsersIX_Email");

            migrationBuilder.RenameIndex(
                name: "IX_User",
                table: "Tokens",
                newName: "TokensIX_User");

            migrationBuilder.RenameIndex(
                name: "IX_NormalizedName",
                table: "Roles",
                newName: "RolesIX_NormalizedName");

            migrationBuilder.RenameIndex(
                name: "IX_Name",
                table: "Roles",
                newName: "RolesIX_Name");

            migrationBuilder.RenameIndex(
                name: "IX_NumberStage",
                table: "Projects",
                newName: "ProjectsIX_NumberStage");

            migrationBuilder.RenameIndex(
                name: "IX_Stage",
                table: "Projects",
                newName: "ProjectsIX_Stage");

            migrationBuilder.RenameIndex(
                name: "IX_License",
                table: "ProjectBranchRoles",
                newName: "ProjectBranchRolesIX_License");

            migrationBuilder.RenameIndex(
                name: "IX_Project",
                table: "ProjectBranches",
                newName: "ProjectBranchesIX_Project");

            migrationBuilder.RenameIndex(
                name: "IX_Branch",
                table: "ProjectBranches",
                newName: "ProjectBranchesIX_Branch");

            migrationBuilder.RenameIndex(
                name: "IX_Designer",
                table: "Licenses",
                newName: "LicensesIX_Designer");

            migrationBuilder.RenameIndex(
                name: "IX_License",
                table: "LicenseBranches",
                newName: "LicenseBranchesIX_License");

            migrationBuilder.RenameIndex(
                name: "IX_Branch",
                table: "LicenseBranches",
                newName: "LicenseBranchesIX_Branch");

            migrationBuilder.RenameIndex(
                name: "IX_NAME",
                table: "Investors",
                newName: "InvestorsIX_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Designer",
                table: "Certificates",
                newName: "CertificatesIX_Designer");

            migrationBuilder.RenameIndex(
                name: "IX_ORDER",
                table: "Branches",
                newName: "BranchesIX_Order");

            migrationBuilder.CreateTable(
                name: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    Master = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    AppDictionaryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionary_Dictionary_AppDictionaryId",
                        column: x => x.AppDictionaryId,
                        principalTable: "Dictionary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPart_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartBranch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    ProjectPartId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartBranch_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartBranch_ProjectPart_ProjectPartId",
                        column: x => x.ProjectPartId,
                        principalTable: "ProjectPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartBranchRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    PartBranchId = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    LicenseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartBranchRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartBranchRole_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartBranchRole_PartBranch_PartBranchId",
                        column: x => x.PartBranchId,
                        principalTable: "PartBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dictionary_AppDictionaryId",
                table: "Dictionary",
                column: "AppDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartBranch_BranchId",
                table: "PartBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PartBranch_ProjectPartId",
                table: "PartBranch",
                column: "ProjectPartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartBranchRole_LicenseId",
                table: "PartBranchRole",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_PartBranchRole_PartBranchId",
                table: "PartBranchRole",
                column: "PartBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPart_ProjectId",
                table: "ProjectPart",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Projects_ProjectId",
                table: "ProjectBranches",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranchRoles_ProjectBranches_ProjectBranchId",
                table: "ProjectBranchRoles",
                column: "ProjectBranchId",
                principalTable: "ProjectBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Projects_ProjectId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranchRoles_ProjectBranches_ProjectBranchId",
                table: "ProjectBranchRoles");

            migrationBuilder.DropTable(
                name: "Dictionary");

            migrationBuilder.DropTable(
                name: "PartBranchRole");

            migrationBuilder.DropTable(
                name: "PartBranch");

            migrationBuilder.DropTable(
                name: "ProjectPart");

            migrationBuilder.RenameIndex(
                name: "UsersIX_UserName",
                table: "Users",
                newName: "IX_UserName");

            migrationBuilder.RenameIndex(
                name: "UsersIX_MormalizedUserName",
                table: "Users",
                newName: "IX_MormalizedUserName");

            migrationBuilder.RenameIndex(
                name: "UsersIX_NormalizedEmail",
                table: "Users",
                newName: "IX_NormalizedEmail");

            migrationBuilder.RenameIndex(
                name: "UsersIX_Email",
                table: "Users",
                newName: "IX_Email");

            migrationBuilder.RenameIndex(
                name: "TokensIX_User",
                table: "Tokens",
                newName: "IX_User");

            migrationBuilder.RenameIndex(
                name: "RolesIX_NormalizedName",
                table: "Roles",
                newName: "IX_NormalizedName");

            migrationBuilder.RenameIndex(
                name: "RolesIX_Name",
                table: "Roles",
                newName: "IX_Name");

            migrationBuilder.RenameIndex(
                name: "ProjectsIX_NumberStage",
                table: "Projects",
                newName: "IX_NumberStage");

            migrationBuilder.RenameIndex(
                name: "ProjectsIX_Stage",
                table: "Projects",
                newName: "IX_Stage");

            migrationBuilder.RenameIndex(
                name: "ProjectBranchRolesIX_License",
                table: "ProjectBranchRoles",
                newName: "IX_License");

            migrationBuilder.RenameIndex(
                name: "ProjectBranchesIX_Project",
                table: "ProjectBranches",
                newName: "IX_Project");

            migrationBuilder.RenameIndex(
                name: "ProjectBranchesIX_Branch",
                table: "ProjectBranches",
                newName: "IX_Branch");

            migrationBuilder.RenameIndex(
                name: "LicensesIX_Designer",
                table: "Licenses",
                newName: "IX_Designer");

            migrationBuilder.RenameIndex(
                name: "LicenseBranchesIX_License",
                table: "LicenseBranches",
                newName: "IX_License");

            migrationBuilder.RenameIndex(
                name: "LicenseBranchesIX_Branch",
                table: "LicenseBranches",
                newName: "IX_Branch");

            migrationBuilder.RenameIndex(
                name: "InvestorsIX_Name",
                table: "Investors",
                newName: "IX_NAME");

            migrationBuilder.RenameIndex(
                name: "CertificatesIX_Designer",
                table: "Certificates",
                newName: "IX_Designer");

            migrationBuilder.RenameIndex(
                name: "BranchesIX_Order",
                table: "Branches",
                newName: "IX_ORDER");

            migrationBuilder.AddForeignKey(
                name: "FK_Project",
                table: "ProjectBranches",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranch",
                table: "ProjectBranchRoles",
                column: "ProjectBranchId",
                principalTable: "ProjectBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
