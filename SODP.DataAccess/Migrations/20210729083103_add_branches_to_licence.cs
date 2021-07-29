using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class add_branches_to_licence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Licence",
                table: "Licences");

            migrationBuilder.DropForeignKey(
                name: "FK_Designer_Licence",
                table: "Licences");

            migrationBuilder.DropIndex(
                name: "IX_Branch",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Licences");

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
                name: "BranchLicence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTimeStamp = table.Column<DateTime>(nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    LicenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchLicence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchLicence_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchLicence_Licences_LicenceId",
                        column: x => x.LicenceId,
                        principalTable: "Licences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "BranchLicence",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Licence",
                table: "BranchLicence",
                column: "LicenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licences_Designers_DesignerId",
                table: "Licences",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licences_Designers_DesignerId",
                table: "Licences");

            migrationBuilder.DropTable(
                name: "BranchLicence");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "BranchDesigners");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "BranchDesigners");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Licences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "Licences",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Licence",
                table: "Licences",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Designer_Licence",
                table: "Licences",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
