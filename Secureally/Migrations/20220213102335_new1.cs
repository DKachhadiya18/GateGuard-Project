using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secureally.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AdminsMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AdminsMaster",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
