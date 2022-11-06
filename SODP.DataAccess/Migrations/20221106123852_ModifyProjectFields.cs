using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ModifyProjectFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Stages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TitleStudy",
                table: "Projects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Investment",
                table: "Projects",
                newName: "LocationUnit");

            migrationBuilder.AddColumn<string>(
                name: "BuildingCategory",
                table: "Projects",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingCategory",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "TitleStudy");

            migrationBuilder.RenameColumn(
                name: "LocationUnit",
                table: "Projects",
                newName: "Investment");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Projects",
                type: "nvarchar(250)",
                nullable: true);
        }
    }
}
