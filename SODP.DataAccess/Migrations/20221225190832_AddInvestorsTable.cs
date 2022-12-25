using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class AddInvestorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_NAME",
                table: "Investors",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investors");

            migrationBuilder.DropColumn(
                name: "DevelopmentDate",
                table: "Projects");

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
        }
    }
}
