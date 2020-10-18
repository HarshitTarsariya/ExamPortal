using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{
    public class MCQAnswerSheetDTO : AnswerSheetDTO
    {
        public int MarksObtained { get; set; }
        public MCQPaperDTO MCQPaper { get; set; }
        public int TotalMarks { get; set; }
    }
}
