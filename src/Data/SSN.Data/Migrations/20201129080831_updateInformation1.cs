using Microsoft.EntityFrameworkCore.Migrations;

namespace SSN.Data.Migrations
{
    public partial class updateInformation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "VoteUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoteUsers_PostId",
                table: "VoteUsers",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteUsers_Posts_PostId",
                table: "VoteUsers",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteUsers_Posts_PostId",
                table: "VoteUsers");

            migrationBuilder.DropIndex(
                name: "IX_VoteUsers_PostId",
                table: "VoteUsers");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "VoteUsers");
        }
    }
}
