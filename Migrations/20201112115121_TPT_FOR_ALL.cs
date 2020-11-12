using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class TPT_FOR_ALL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescriptiveAnswerSheet_AnswerSheet_Id",
                table: "DescriptiveAnswerSheet");

            migrationBuilder.DropForeignKey(
                name: "FK_DescriptivePaper_Paper_Id",
                table: "DescriptivePaper");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQAnswerSheet_AnswerSheet_Id",
                table: "MCQAnswerSheet");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQPaper_Paper_Id",
                table: "MCQPaper");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MCQPaper_MCQPaperId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_MCQOptionId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_MCQPaperId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MCQOptionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MCQPaperId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Questions",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Paper",
                newName: "PaperId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MCQPaper",
                newName: "PaperId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MCQOptions",
                newName: "MCQOptionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MCQAnswerSheet",
                newName: "AnswerSheetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DescriptivePaper",
                newName: "PaperId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DescriptiveAnswerSheet",
                newName: "AnswerSheetId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnswerSheet",
                newName: "AnswerSheetId");

            migrationBuilder.CreateTable(
                name: "MCQQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    MCQPaperId = table.Column<int>(type: "int", nullable: false),
                    MCQOptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_MCQQuestions_MCQOptions_MCQOptionId",
                        column: x => x.MCQOptionId,
                        principalTable: "MCQOptions",
                        principalColumn: "MCQOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MCQQuestions_MCQPaper_MCQPaperId",
                        column: x => x.MCQPaperId,
                        principalTable: "MCQPaper",
                        principalColumn: "PaperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MCQQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestions_MCQOptionId",
                table: "MCQQuestions",
                column: "MCQOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQQuestions_MCQPaperId",
                table: "MCQQuestions",
                column: "MCQPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_DescriptiveAnswerSheet_AnswerSheet_AnswerSheetId",
                table: "DescriptiveAnswerSheet",
                column: "AnswerSheetId",
                principalTable: "AnswerSheet",
                principalColumn: "AnswerSheetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DescriptivePaper_Paper_PaperId",
                table: "DescriptivePaper",
                column: "PaperId",
                principalTable: "Paper",
                principalColumn: "PaperId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQAnswerSheet_AnswerSheet_AnswerSheetId",
                table: "MCQAnswerSheet",
                column: "AnswerSheetId",
                principalTable: "AnswerSheet",
                principalColumn: "AnswerSheetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_MCQQuestions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId",
                principalTable: "MCQQuestions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQPaper_Paper_PaperId",
                table: "MCQPaper",
                column: "PaperId",
                principalTable: "Paper",
                principalColumn: "PaperId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescriptiveAnswerSheet_AnswerSheet_AnswerSheetId",
                table: "DescriptiveAnswerSheet");

            migrationBuilder.DropForeignKey(
                name: "FK_DescriptivePaper_Paper_PaperId",
                table: "DescriptivePaper");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQAnswerSheet_AnswerSheet_AnswerSheetId",
                table: "MCQAnswerSheet");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_MCQQuestions_MCQQuestionId",
                table: "MCQOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQPaper_Paper_PaperId",
                table: "MCQPaper");

            migrationBuilder.DropTable(
                name: "MCQQuestions");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Questions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaperId",
                table: "Paper",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaperId",
                table: "MCQPaper",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MCQOptionId",
                table: "MCQOptions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnswerSheetId",
                table: "MCQAnswerSheet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaperId",
                table: "DescriptivePaper",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnswerSheetId",
                table: "DescriptiveAnswerSheet",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnswerSheetId",
                table: "AnswerSheet",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MCQOptionId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MCQPaperId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MCQOptionId",
                table: "Questions",
                column: "MCQOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MCQPaperId",
                table: "Questions",
                column: "MCQPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_DescriptiveAnswerSheet_AnswerSheet_Id",
                table: "DescriptiveAnswerSheet",
                column: "Id",
                principalTable: "AnswerSheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DescriptivePaper_Paper_Id",
                table: "DescriptivePaper",
                column: "Id",
                principalTable: "Paper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQAnswerSheet_AnswerSheet_Id",
                table: "MCQAnswerSheet",
                column: "Id",
                principalTable: "AnswerSheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MCQPaper_Paper_Id",
                table: "MCQPaper",
                column: "Id",
                principalTable: "Paper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions",
                column: "MCQOptionId",
                principalTable: "MCQOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MCQPaper_MCQPaperId",
                table: "Questions",
                column: "MCQPaperId",
                principalTable: "MCQPaper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
