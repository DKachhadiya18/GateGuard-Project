using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secureally.Migrations
{
    public partial class upda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SocietyMaster",
                newName: "SocietyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocietyId",
                table: "SocietyMaster",
                newName: "Id");
        }
    }
}
