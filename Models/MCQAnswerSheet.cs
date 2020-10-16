using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Models
{
    public class MCQAnswerSheet : AnswerSheet
    {
        public int MarksObtained { get; set; }
        [Required]
        public int MCQPaperId { get; set; }
        public MCQPaper MCQPaper { get; set; }
    }
}
