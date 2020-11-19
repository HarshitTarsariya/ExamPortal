using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class ChangeKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions");

            migrationBuilder.AlterColumn<int>(
                name: "MCQQuestionId",
                table: "MCQOptions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions");

            migrationBuilder.AlterColumn<int>(
                name: "MCQQuestionId",
                table: "MCQOptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
