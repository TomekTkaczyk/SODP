using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class add_branches_to_license : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_License",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Designer_License",
                table: "Licenses");

            migrationBuilder.DropIndex(
                name: "IX_Branch",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Licenses");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "BranchDesigners",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "BranchDesigners",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BranchLicense",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    LicenseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchLicense", x => x.Id);
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
                name: "IX_Branch",
                table: "BranchLicense",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_License",
                table: "BranchLicense",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Designers_DesignerId",
                table: "Licenses",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Designers_DesignerId",
                table: "Licenses");

            migrationBuilder.DropTable(
                name: "BranchLicense");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "BranchDesigners");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "BranchDesigners");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Licenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "Licenses",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_License",
                table: "Licenses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Designer_License",
                table: "Licenses",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
