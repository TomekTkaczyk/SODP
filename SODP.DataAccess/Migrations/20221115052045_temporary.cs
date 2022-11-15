using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class temporary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licenses_CheckingLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectBranches_Licenses_DesignerLicenseId",
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
                name: "CheckingLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropColumn(
                name: "DesignerLicenseId",
                table: "ProjectBranches");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Branches");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Licenses",
                type: "nvarchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "LicenseBranches",
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
                    table.PrimaryKey("PK_LicenseBranches", x => new { x.BranchId, x.LicenseId });
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

            migrationBuilder.CreateIndex(
                name: "IX_ORDER",
                table: "Branches",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "LicenseBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "LicenseBranches",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "ProjectBranchRoles",
                column: "LicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseBranches");

            migrationBuilder.DropTable(
                name: "ProjectBranchRoles");

            migrationBuilder.DropIndex(
                name: "IX_ORDER",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "CheckingLicenseId",
                table: "ProjectBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesignerLicenseId",
                table: "ProjectBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Licenses",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Branches",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Branches",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "00");

            migrationBuilder.CreateTable(
                name: "BranchLicense",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    CreateTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
        }
    }
}
