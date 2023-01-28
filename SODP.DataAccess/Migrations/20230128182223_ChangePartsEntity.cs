using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ChangePartsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParts_Parts_PartId",
                table: "ProjectParts");

            migrationBuilder.DropIndex(
                name: "ProjectPartsIX_Part",
                table: "ProjectParts");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectParts");

            migrationBuilder.DropColumn(
                name: "Sign",
                table: "ProjectParts");

            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "ProjectParts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
