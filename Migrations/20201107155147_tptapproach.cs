using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class tptapproach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSheets_Papers_MCQPaperId",
                table: "AnswerSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Papers_MCQPaperId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Papers",
                table: "Papers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerSheets",
                table: "AnswerSheets");

            migrationBuilder.DropIndex(
                name: "IX_AnswerSheets_MCQPaperId",
                table: "AnswerSheets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AnswerSheets");

            migrationBuilder.DropColumn(
                name: "MCQPaperId",
                table: "AnswerSheets");

            migrationBuilder.DropColumn(
                name: "MarksObtained",
                table: "AnswerSheets");

            migrationBuilder.RenameTable(
                name: "Papers",
                newName: "Paper");

            migrationBuilder.RenameTable(
                name: "AnswerSheets",
                newName: "AnswerSheet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paper",
                table: "Paper",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerSheet",
                table: "AnswerSheet",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DescriptivePaper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMarks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptivePaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptivePaper_Paper_Id",
                        column: x => x.Id,
                        principalTable: "Paper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MCQPaper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQPaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MCQPaper_Paper_Id",
                        column: x => x.Id,
                        principalTable: "Paper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DescriptiveAnswerSheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AnswerLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarksObtained = table.Column<int>(type: "int", nullable: true),
                    DescriptivePaperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptiveAnswerSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptiveAnswerSheet_AnswerSheet_Id",
                        column: x => x.Id,
                        principalTable: "AnswerSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DescriptiveAnswerSheet_DescriptivePaper_DescriptivePaperId",
                        column: x => x.DescriptivePaperId,
                        principalTable: "DescriptivePaper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MCQAnswerSheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MarksObtained = table.Column<int>(type: "int", nullable: false),
                    MCQPaperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQAnswerSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MCQAnswerSheet_AnswerSheet_Id",
                        column: x => x.Id,
                        principalTable: "AnswerSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MCQAnswerSheet_MCQPaper_MCQPaperId",
                        column: x => x.MCQPaperId,
                        principalTable: "MCQPaper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescriptiveAnswerSheet_DescriptivePaperId",
                table: "DescriptiveAnswerSheet",
                column: "DescriptivePaperId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQAnswerSheet_MCQPaperId",
                table: "MCQAnswerSheet",
                column: "MCQPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_MCQPaper_MCQPaperId",
                table: "Questions",
                column: "MCQPaperId",
                principalTable: "MCQPaper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_MCQPaper_MCQPaperId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "DescriptiveAnswerSheet");

            migrationBuilder.DropTable(
                name: "MCQAnswerSheet");

            migrationBuilder.DropTable(
                name: "DescriptivePaper");

            migrationBuilder.DropTable(
                name: "MCQPaper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paper",
                table: "Paper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerSheet",
                table: "AnswerSheet");

            migrationBuilder.RenameTable(
                name: "Paper",
                newName: "Papers");

            migrationBuilder.RenameTable(
                name: "AnswerSheet",
                newName: "AnswerSheets");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AnswerSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MCQPaperId",
                table: "AnswerSheets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarksObtained",
                table: "AnswerSheets",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Papers",
                table: "Papers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerSheets",
                table: "AnswerSheets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSheets_MCQPaperId",
                table: "AnswerSheets",
                column: "MCQPaperId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSheets_Papers_MCQPaperId",
                table: "AnswerSheets",
                column: "MCQPaperId",
                principalTable: "Papers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Papers_MCQPaperId",
                table: "Questions",
                column: "MCQPaperId",
                principalTable: "Papers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
