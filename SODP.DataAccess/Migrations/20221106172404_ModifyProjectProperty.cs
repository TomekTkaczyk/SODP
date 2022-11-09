using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ModifyProjectProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Licences_Branches_BranchId",
                table: "Licences");

            migrationBuilder.RenameTable(
                name: "Licences",
                newName: "Licenses");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Licenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Licenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.RenameColumn(
                name: "Contents",
                table: "Licenses",
                newName: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Branch",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Licenses");

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

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Stages",
                newName: "Name");

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

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Projects",
                newName: "Name");

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

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CheckingLicenseId",
                table: "ProjectBranches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesignerLicenseId",
                table: "ProjectBranches",
                nullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Sign",
                table: "Branches",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

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

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Branches",
                type: "varchar(2)",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "BranchLicense",
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
                    table.PrimaryKey("PK_BranchLicense", x => new { x.BranchId, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_BranchLicense_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchLicense_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checking",
                table: "ProjectBranches",
                column: "CheckingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Designer",
                table: "ProjectBranches",
                column: "DesignerLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_SYMBOL",
                table: "Branches",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "BranchLicense",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "BranchLicense",
                column: "LicenseId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Project",
                table: "ProjectBranches",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licenses_CheckingLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licenses_DesignerLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_Project",
                table: "ProjectBranches");

            migrationBuilder.DropTable(
                name: "BranchLicense");

            migrationBuilder.DropIndex(
                name: "IX_Checking",
                table: "ProjectBranches");

            migrationBuilder.DropIndex(
                name: "IX_Designer",
                table: "ProjectBranches");

            migrationBuilder.DropIndex(
                name: "IX_SYMBOL",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Stages");

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
                table: "Projects",
                newName: "Title");

            migrationBuilder.DropColumn(
                name: "CheckingLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropColumn(
                name: "DesignerLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Designers");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Certificates");

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
                name: "Symbol",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stages",
                newName: "Title");

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

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Projects",
                type: "nvarchar(250)",
                nullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Certificates",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

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
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Licenses");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Licenses",
                newName: "Contents");

            migrationBuilder.RenameTable(
                name: "Licenses",
                newName: "Licences");

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
        }
    }
}
