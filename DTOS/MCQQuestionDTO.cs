using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{
    public class MCQQuestionDTO : QuestionDTO
    {
        public MCQQuestionDTO()
        {
            Opetions = new List<string>();
        }
        public List<string> Opetions { get; set; }
        public int TrueAnswer { get; set; }
    }
}
