using Microsoft.EntityFrameworkCore.Migrations;

namespace SSN.Data.Migrations
{
    public partial class UserAccAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserAcc",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "UserAcc",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "UserAcc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "UserAcc",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "UserAcc");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserAcc");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "UserAcc");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "UserAcc");
        }
    }
}
