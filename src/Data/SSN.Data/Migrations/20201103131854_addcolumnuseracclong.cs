using Microsoft.EntityFrameworkCore.Migrations;

namespace SSN.Data.Migrations
{
    public partial class addcolumnuseracclong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UIN",
                table: "UserAcc",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UIN",
                table: "UserAcc",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
