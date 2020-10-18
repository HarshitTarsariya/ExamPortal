using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class nullableForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "MCQPaperId",
                table: "AnswerSheets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "MCQPaperId",
                table: "AnswerSheets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
