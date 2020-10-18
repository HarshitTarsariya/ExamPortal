using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Models
{
    public class MCQOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int MCQQuestionId { get; set; }
        public MCQQuestion MCQQuestion { get; set; }
    }
}
