using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class AddDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_Project",
                table: "ProjectBranches",
                newName: "ProjectBranchesIX_Project");

            migrationBuilder.RenameIndex(
                name: "IX_Branch",
                table: "ProjectBranches",
                newName: "ProjectBranchesIX_Branch");

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
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseBranches_Branches_BranchId",
                table: "LicenseBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseBranches_Licenses_LicenseId",
                table: "LicenseBranches");

            migrationBuilder.RenameIndex(
                name: "IX_License",
                table: "LicenseBranches",
                newName: "LicenseBranchesIX_License");

            migrationBuilder.RenameIndex(
                name: "IX_Branch",
                table: "LicenseBranches",
                newName: "LicenseBranchesIX_Branch");

            migrationBuilder.AddForeignKey(
                 name: "FK_LicenseBranches_Branches_BranchId",
                 table: "LicenseBranches",
                 column: "BranchId",
                 principalTable: "Branches",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                 name: "FK_LicenseBranches_Licenses_LicenseId",
                 table: "LicenseBranches",
                 column: "LicenseId",
                 principalTable: "Licenses",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

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
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

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
                name: "Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "DictionaryIX_DictionaryId",
                table: "Dictionary",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "InvestorsIX_Name",
                table: "Investors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "PartBranchIX_BranchId",
                table: "PartBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "PartBranchIX_ProjectPartId",
                table: "PartBranch",
                column: "ProjectPartId");

            migrationBuilder.CreateIndex(
                name: "PartBranchRoleIX_LicenseId",
                table: "PartBranchRole",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "PartBranchRoleIX_PartBranchId",
                table: "PartBranchRole",
                column: "PartBranchId");

            migrationBuilder.CreateIndex(
                name: "ProjectPartIX_ProjectId",
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
                name: "Investors");

            migrationBuilder.DropTable(
                name: "PartBranchRole");

            migrationBuilder.DropTable(
                name: "PartBranch");

            migrationBuilder.DropTable(
                name: "ProjectPart");

			migrationBuilder.DropTable(
            	name: "Parts");

			migrationBuilder.DropColumn(
                name: "DevelopmentDate",
                table: "Projects");

			migrationBuilder.DropColumn(
                name: "Order",
                table: "Stages");

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
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "ProjectBranchesIX_Project",
                table: "ProjectBranches",
                newName: "IX_Project");

            migrationBuilder.RenameIndex(
                name: "ProjectBranchesIX_Branch",
                table: "ProjectBranches",
                newName: "IX_Branch");

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
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseBranches_Branches_BranchId",
                table: "LicenseBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseBranches_Licenses_LicenseId",
                table: "LicenseBranches");

            migrationBuilder.RenameIndex(
                name: "LicenseBranchesIX_License",
                table: "LicenseBranches",
                newName: "IX_License");

            migrationBuilder.RenameIndex(
                name: "LicenseBranchesIX_Branch",
                table: "LicenseBranches",
                newName: "IX_Branch");

            migrationBuilder.AddForeignKey(
                 name: "FK_LicenseBranches_Branches_BranchId",
                 table: "LicenseBranches",
                 column: "BranchId",
                 principalTable: "Branches",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                 name: "FK_LicenseBranches_Licenses_LicenseId",
                 table: "LicenseBranches",
                 column: "LicenseId",
                 principalTable: "Licenses",
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

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
                 principalColumn: "Id",
                 onDelete: ReferentialAction.Cascade);

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
