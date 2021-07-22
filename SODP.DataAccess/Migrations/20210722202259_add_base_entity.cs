using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class add_base_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Branches",
                newName: "Title");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamp",
                table: "Licences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTimeStamp",
                table: "Licences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "Branches",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "CreateTimeStamp",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Licences");

            migrationBuilder.DropColumn(
                name: "ModifyTimeStamp",
                table: "Licences");

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

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Branches",
                newName: "Name");
        }
    }
}
