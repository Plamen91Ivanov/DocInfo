using Microsoft.EntityFrameworkCore.Migrations;

namespace SSN.Data.Migrations
{
    public partial class addcolumnuseracc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UIN",
                table: "UserAcc",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UIN",
                table: "UserAcc");
        }
    }
}
