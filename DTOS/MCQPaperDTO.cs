using System.Collections.Generic;

namespace ExamPortal.DTOS
{

    public class MCQPaperDTO : PaperDTO
    {
        public MCQPaperDTO()
        {
            Questions = new List<MCQQuestionDTO>();
        }

        public List<MCQQuestionDTO> Questions { get; set; }

    }
}
