using Microsoft.EntityFrameworkCore.Migrations;

namespace SODP.DataAccess.Migrations
{
    public partial class add_licences_to_designer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licences_Designers_DesignerId",
                table: "Licences");

            migrationBuilder.AddForeignKey(
                name: "FK_Designer_Licence",
                table: "Licences",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designer_Licence",
                table: "Licences");

            migrationBuilder.AddForeignKey(
                name: "FK_Licences_Designers_DesignerId",
                table: "Licences",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
