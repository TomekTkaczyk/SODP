using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class ChangePartBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Name",
            //    table: "ProjectParts");

			migrationBuilder.RenameColumn(
				name: "Name",
				table: "ProjectParts",
				newName: "Title");

			migrationBuilder.RenameColumn(
                name: "Name",
                table: "Stages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Parts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dictionary",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Branches",
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
                type: "nvarchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true,
                oldDefaultValue: "");

			migrationBuilder.AlterColumn<string>(
				name: "Title",
				table: "ProjectParts",
				type: "nvarchar(50)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldNullable: true,
				oldDefaultValue: "");

			//migrationBuilder.AddColumn<string>(
   //             name: "Title",
   //             table: "ProjectParts",
   //             type: "nvarchar(50)",
   //             nullable: false,
   //             defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Designers",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true,
                oldDefaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Title",
            //    table: "ProjectParts");

			migrationBuilder.RenameColumn(
				name: "Title",
				table: "ProjectParts",
				newName: "Name");

			migrationBuilder.RenameColumn(
                name: "Title",
                table: "Stages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Parts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Dictionary",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Branches",
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

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Projects",
                type: "nvarchar(256)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldNullable: true);

			//migrationBuilder.AddColumn<string>(
			//    name: "Name",
			//    table: "ProjectParts",
			//    type: "nvarchar(50)",
			//    nullable: false,
			//    defaultValue: "");

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "ProjectParts",
				type: "nvarchar(50)",
				nullable: true,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Designers",
                type: "nvarchar(20)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);
        }
    }
}
