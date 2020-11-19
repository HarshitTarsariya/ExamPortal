using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class Clear1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions",
                column: "MCQOptionId",
                principalTable: "MCQOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions",
                column: "MCQOptionId",
                principalTable: "MCQOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
