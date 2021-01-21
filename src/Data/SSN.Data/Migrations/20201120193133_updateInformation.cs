using Microsoft.EntityFrameworkCore.Migrations;

namespace SSN.Data.Migrations
{
    public partial class updateInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Neg",
                table: "information",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Neg",
                table: "information",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
