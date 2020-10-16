using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class McqTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Papers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaperCode = table.Column<string>(nullable: true),
                    TeacherEmailId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DeadLine = table.Column<DateTime>(nullable: false),
                    PaperTitle = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentEmailId = table.Column<string>(nullable: true),
                    SubmittedTime = table.Column<DateTime>(nullable: false),
                    MCQPaperId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    MarksObtained = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSheets_Papers_MCQPaperId",
                        column: x => x.MCQPaperId,
                        principalTable: "Papers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(nullable: true),
                    Marks = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    MCQPaperId = table.Column<int>(nullable: true),
                    MCQOptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Papers_MCQPaperId",
                        column: x => x.MCQPaperId,
                        principalTable: "Papers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MCQOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionText = table.Column<string>(nullable: true),
                    MCQQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MCQOptions_Questions_MCQQuestionId",
                        column: x => x.MCQQuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSheets_MCQPaperId",
                table: "AnswerSheets",
                column: "MCQPaperId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQOptions_MCQQuestionId",
                table: "MCQOptions",
                column: "MCQQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MCQOptionId",
                table: "Questions",
                column: "MCQOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MCQPaperId",
                table: "Questions",
                column: "MCQPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MCQOptions_MCQOptionId",
                table: "Questions",
                column: "MCQOptionId",
                principalTable: "MCQOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Papers_MCQPaperId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_MCQOptions_Questions_MCQQuestionId",
                table: "MCQOptions");

            migrationBuilder.DropTable(
                name: "AnswerSheets");

            migrationBuilder.DropTable(
                name: "Papers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "MCQOptions");
        }
    }
}
