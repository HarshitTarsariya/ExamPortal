namespace ExamPortal.DTOS
{
    public class MCQAnswerSheetDTO : AnswerSheetDTO
    {
        public int MarksObtained { get; set; }
        public MCQPaperDTO Paper { get; set; }
        public int TotalMarks { get; set; }
    }
}
