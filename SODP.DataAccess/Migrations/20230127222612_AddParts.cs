using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class AddParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseBranches");

            migrationBuilder.DropTable(
                name: "ProjectBranchRoles");

            migrationBuilder.DropTable(
                name: "ProjectBranches");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.RenameIndex(
                name: "IX_User",
                table: "Tokens",
                newName: "TokensIX_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

			migrationBuilder.DropForeignKey(
				name: "FK_Projects_Stages_StageId",
				table: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Stage",
                table: "Projects",
                newName: "ProjectsIX_Stage");

			migrationBuilder.AddForeignKey(
				name: "FK_Projects_Stages_StageId",
				table: "Projects",
				column: "StageId",
				principalTable: "Stages",
				principalColumn: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Designers_DesignerId",
                table: "Licenses");

            migrationBuilder.RenameIndex(
                name: "IX_Designer",
                table: "Licenses",
                newName: "LicensesIX_Designer");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Designers_DesignerId",
                table: "Licenses",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Designers_DesignerId",
                table: "Certificates");

            migrationBuilder.RenameIndex(
                name: "IX_Designer",
                table: "Certificates",
                newName: "CertificatesIX_Designer");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Designers_DesignerId",
                table: "Certificates",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ORDER",
                table: "Branches",
                newName: "BranchesIX_Order");

            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Stages",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveStatus",
                table: "Stages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Stages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "DevelopmentDate",
                table: "Projects",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveStatus",
                table: "Designers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.CreateTable(
                name: "BranchLicenses",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false),
                    LicenseId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchLicenses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchLicenses_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Sign = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ActiveStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dictionary_Dictionary_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Dictionary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Sign = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    PartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectParts_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartBranches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    ProjectPartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartBranches_ProjectParts_ProjectPartId",
                        column: x => x.ProjectPartId,
                        principalTable: "ProjectParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchRoles",
                columns: table => new
                {
                    PartBranchId = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    LicenseId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchRoles_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchRoles_PartBranches_PartBranchId",
                        column: x => x.PartBranchId,
                        principalTable: "PartBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "BranchLicensesIX_Branch",
                table: "BranchLicenses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "BranchLicensesIX_License",
                table: "BranchLicenses",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "BranchRolesIX_License",
                table: "BranchRoles",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "DictioanryIX_ParentId",
                table: "Dictionary",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "InvestorsIX_Name",
                table: "Investors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "PartBranchesIX_ProjectPartId",
                table: "PartBranches",
                column: "ProjectPartId");

            migrationBuilder.CreateIndex(
                name: "PartIX_Order",
                table: "Parts",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "ProjectPartsIX_Part",
                table: "ProjectParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "ProjectPartsIX_Project",
                table: "ProjectParts",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchLicenses");

            migrationBuilder.DropTable(
                name: "BranchRoles");

            migrationBuilder.DropTable(
                name: "Dictionary");

            migrationBuilder.DropTable(
                name: "Investors");

            migrationBuilder.DropTable(
                name: "PartBranches");

            migrationBuilder.DropTable(
                name: "ProjectParts");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "DevelopmentDate",
                table: "Projects");

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

			migrationBuilder.DropForeignKey(
				name: "FK_Tokens_Users_UserId",
				table: "Tokens");

			migrationBuilder.RenameIndex(
				name: "TokensIX_User",
				table: "Tokens",
				newName: "IX_User");

			migrationBuilder.AddForeignKey(
				name: "FK_Tokens_Users_UserId",
				table: "Tokens",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

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

			migrationBuilder.DropForeignKey(
	            name: "FK_Projects_Stages_StageId",
	            table: "Projects");

			migrationBuilder.RenameIndex(
				name: "ProjectsIX_Stage",
				table: "Projects",
				newName: "IX_Stage");

			migrationBuilder.AddForeignKey(
				name: "FK_Projects_Stages_StageId",
				table: "Projects",
				column: "StageId",
				principalTable: "Stages",
				principalColumn: "Id");

			migrationBuilder.DropForeignKey(
			   name: "FK_Licenses_Designers_DesignerId",
			   table: "Licenses");

			migrationBuilder.RenameIndex(
				name: "LicensesIX_Designer",
				table: "Licenses",
				newName: "IX_Designer");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Designers_DesignerId",
                table: "Licenses",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id");

            migrationBuilder.DropForeignKey(
				name: "FK_Certificates_Designers_DesignerId",
				table: "Certificates");

			migrationBuilder.RenameIndex(
				name: "CertificatesIX_Designer",
				table: "Certificates",
				newName: "IX_Designer");

			migrationBuilder.AddForeignKey(
				name: "FK_Certificates_Designers_DesignerId",
				table: "Certificates",
				column: "DesignerId",
				principalTable: "Designers",
				principalColumn: "Id");

			migrationBuilder.RenameIndex(
                name: "BranchesIX_Order",
                table: "Branches",
                newName: "IX_ORDER");

            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Stages",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveStatus",
                table: "Stages",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveStatus",
                table: "Designers",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "LicenseBranches",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreateTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseBranches", x => new { x.LicenseId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_LicenseBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseBranches_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBranchRoles",
                columns: table => new
                {
                    ProjectBranchId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBranchRoles", x => new { x.ProjectBranchId, x.Role, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_ProjectBranchRoles_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectBranch",
                        column: x => x.ProjectBranchId,
                        principalTable: "ProjectBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "LicenseBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "LicenseBranches",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "ProjectBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Project",
                table: "ProjectBranches",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "ProjectBranchRoles",
                column: "LicenseId");
        }
    }
}
