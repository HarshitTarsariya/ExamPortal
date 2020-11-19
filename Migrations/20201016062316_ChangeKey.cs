using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class ChangeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions");

            migrationBuilder.AlterColumn<int>(
                name: "MCQQuestionId",
                table: "MCQOptions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
