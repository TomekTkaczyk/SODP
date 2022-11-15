using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ModifyProjectProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region Stages

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Stages",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Stages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Stages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Stages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            #endregion

            #region Projects

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Projects",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuildingCategory",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Investor",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationUnit",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            #endregion

            #region Licenses

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    DesignerId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_Designers_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "Designers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designer",
                table: "Licenses",
                column: "DesignerId");

            migrationBuilder.Sql("REPLACE INTO `Licenses` (Id, DesignerId, Content, CreateTimeStamp, ModifyTimeStamp) SELECT Id, DesignerId, Contents, CURDATE(), CURDATE() FROM `Licences`;");

            #endregion

            #region Users

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            #endregion

            #region Tokens

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Tokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Tokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            #endregion

            #region Designers

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Designers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Designers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Designers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            #endregion

            #region Certificates

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Certificates",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Certificates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Certificates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            #endregion

            #region Branches

            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Branches",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Branches",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Branches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Branches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql("UPDATE `Branches` SET Branches.Order = Id");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER",
                table: "Branches",
                column: "Order",
                unique: false);

            #endregion

            #region LicenseBranches

            migrationBuilder.CreateTable(
                name: "LicenseBranches",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false),
                    LicenseId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
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

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "LicenseBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "LicenseBranches",
                column: "LicenseId");

            #endregion

            #region ProjectBranches

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licences_CheckingLicenceId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licences_DesignerLicenceId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Projects_ProjectId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Branches_BranchId",
                table: "ProjectBranches"); 
            
            migrationBuilder.DropIndex(
                name: "IX_Checking",
                table: "ProjectBranches");

            migrationBuilder.DropIndex(
                name: "IX_Designer",
                table: "ProjectBranches");

            migrationBuilder.DropColumn(
                name: "CheckingLicenceId",
                table: "ProjectBranches");

            migrationBuilder.DropColumn(
                name: "DesignerLicenceId",
                table: "ProjectBranches");

            //migrationBuilder.RenameColumn(
            //    name: "DesignerLicenceId",
            //    newName: "DesignerLicenseId",
            //    table: "ProjectBranches");

            //migrationBuilder.RenameColumn(
            //    name: "CheckingLicenceId",
            //    newName: "CheckingLicenseId",
            //    table: "ProjectBranches");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Checking",
            //    table: "ProjectBranches",
            //    column: "CheckingLicenseId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Designer",
            //    table: "ProjectBranches",
            //    column: "DesignerLicenseId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectBranches_Licenses_CheckingLicenseId",
            //    table: "ProjectBranches",
            //    column: "CheckingLicenseId",
            //    principalTable: "Licenses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectBranches_Licenses_DesignerLicenseId",
            //    table: "ProjectBranches",
            //    column: "DesignerLicenseId",
            //    principalTable: "Licenses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Project",
            //    table: "ProjectBranches",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //   name: "FK_Branch",
            //   table: "ProjectBranches",
            //   column: "BranchId",
            //   principalTable: "Branches",
            //   principalColumn: "Id",
            //   onDelete: ReferentialAction.Cascade);
            #endregion

            #region ProjectBranchRoles

            migrationBuilder.CreateTable(
                name: "ProjectBranchRoles",
                columns: table => new
                {
                    ProjectBranchId = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    LicenseId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
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
            #endregion

            #region Licences

            migrationBuilder.DropTable(
                name: "Licences");

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            #region Licences

            migrationBuilder.CreateTable(
                name: "Licences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(type: "int", nullable: false, defaultValue: null),
                    Contents = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DesignerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licences_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licences_Designers_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "Designers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql("REPLACE INTO `Licences` (Id, DesignerId, Contents, BranchId) SELECT Licenses.Id,Licenses.DesignerId,Content,LicenseBranches.BranchId FROM Licenses,Branches,LicenseBranches WHERE LicenseBranches.BranchId=Branches.Id AND LicenseBranches.LicenseId=Licenses.Id GROUP BY Id;");

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "Licences",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Designer",
                table: "Licences",
                column: "DesignerId");

            #endregion

            #region Branches

            migrationBuilder.DropIndex(
                name: "IX_ORDER",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Branches");

            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Branches",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            #endregion

            #region Tokens

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Stages");

            #endregion

            #region Stages

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Stages");

            #endregion

            #region Projects

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BuildingCategory",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Investor",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LocationUnit",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Name",
                newName: "Title",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Projects",
                type: "nvarchar(250)",
                nullable: true);

            #endregion

            #region Designers

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Designers");

            #endregion

            #region Certificates

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Certificates");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Certificates",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            #endregion

            #region Stage

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stages",
                newName: "Title");

            #endregion

            #region Users

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "nvarchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "nvarchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)");

            #endregion

            #region ProjectBranches

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectBranches_Licenses_CheckingLicenseId",
            //    table: "ProjectBranches");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectBranches_Licenses_DesignerLicenseId",
            //    table: "ProjectBranches");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Project",
            //    table: "ProjectBranches");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Branch",
            //    table: "ProjectBranches");
            
            //migrationBuilder.DropIndex(
            //    name: "IX_Checking",
            //    table: "ProjectBranches");

            //migrationBuilder.DropIndex(
            //    name: "IX_Designer",
            //    table: "ProjectBranches");

            migrationBuilder.AddColumn<int>(
                name: "CheckingLicenceId",
                table: "ProjectBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesignerLicenceId",
                table: "ProjectBranches",
                type: "int",
                nullable: true);

            //migrationBuilder.RenameColumn(
            //    name: "CheckingLicenseId",
            //    newName: "CheckingLicenceId",
            //    table: "ProjectBranches");

            //migrationBuilder.RenameColumn(
            //    name: "DesignerLicenseId",
            //    newName: "DesignerLicenceId",
            //    table: "ProjectBranches");

            migrationBuilder.CreateIndex(
                name: "IX_Checking",
                table: "ProjectBranches",
                column: "CheckingLicenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Designer",
                table: "ProjectBranches",
                column: "DesignerLicenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Licences_CheckingLicenceId",
                table: "ProjectBranches",
                column: "CheckingLicenceId",
                principalTable: "Licences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Licences_DesignerLicenceId",
                table: "ProjectBranches",
                column: "DesignerLicenceId",
                principalTable: "Licences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Projects_ProjectId",
                table: "ProjectBranches",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectBranches_Branches_BranchId",
                table: "ProjectBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            #endregion

            #region ProjectBranchRoles

            migrationBuilder.DropTable(
                name: "ProjectBranchRoles");
            #endregion

            #region LicenseBranches

            migrationBuilder.DropTable(
                name: "LicenseBranches");

            #endregion

            #region Licenses

            migrationBuilder.DropTable(
                name: "Licenses");
            
            #endregion

        }
    }
}
