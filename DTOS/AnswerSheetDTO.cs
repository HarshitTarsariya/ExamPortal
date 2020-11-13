using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{
    public class AnswerSheetDTO
    {
        public string StudentEmailId { get; set; }
        public DateTime SubmittedTime { get; set; }
        public int? MarksObtained { get; set; } = -1;
        public PaperDTO paperdto { get; set; }
    }
}
