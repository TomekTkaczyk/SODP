using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class add_number_to_branches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchDesigners");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Branches",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveStatus",
                table: "Branches",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Branches",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "00");

            migrationBuilder.Sql("SET @counter := 0;UPDATE Branches SET Symbol = LPAD(@counter:= @counter + 1, 2, '0');");
            migrationBuilder.Sql("UPDATE Branches SET ActiveStatus = 1;");

            migrationBuilder.CreateIndex(
                name: "IX_SYMBOL",
                table: "Branches",
                column: "Symbol",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SYMBOL",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Branches");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Branches",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ActiveStatus",
                table: "Branches",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "BranchDesigners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreateTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DesignerId = table.Column<int>(type: "int", nullable: false),
                    ModifyTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchDesigners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchDesigners_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchDesigners_Designers_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "Designers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "BranchDesigners",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Designer",
                table: "BranchDesigners",
                column: "DesignerId");
        }
    }
}
