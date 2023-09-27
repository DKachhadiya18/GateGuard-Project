using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secureally.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardId",
                table: "WorkersRecord");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "OTPRecord",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OTPRecord",
                newName: "MemberId");

            migrationBuilder.AddColumn<int>(
                name: "GuardId",
                table: "WorkersRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
