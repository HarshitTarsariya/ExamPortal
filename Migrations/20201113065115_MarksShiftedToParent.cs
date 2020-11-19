using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPortal.Migrations
{
    public partial class MarksShiftedToParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarksObtained",
                table: "MCQAnswerSheet");

            migrationBuilder.DropColumn(
                name: "TotalMarks",
                table: "DescriptivePaper");

            migrationBuilder.DropColumn(
                name: "MarksObtained",
                table: "DescriptiveAnswerSheet");

            migrationBuilder.AddColumn<int>(
                name: "TotalMarks",
                table: "Paper",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarksObtained",
                table: "AnswerSheet",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMarks",
                table: "Paper");

            migrationBuilder.DropColumn(
                name: "MarksObtained",
                table: "AnswerSheet");

            migrationBuilder.AddColumn<int>(
                name: "MarksObtained",
                table: "MCQAnswerSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalMarks",
                table: "DescriptivePaper",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarksObtained",
                table: "DescriptiveAnswerSheet",
                type: "int",
                nullable: true);
        }
    }
}
