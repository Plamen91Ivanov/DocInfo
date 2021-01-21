using Microsoft.EntityFrameworkCore.Migrations;

namespace SSN.Data.Migrations
{
    public partial class filePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "UserAcc",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "UserAcc");
        }
    }
}
