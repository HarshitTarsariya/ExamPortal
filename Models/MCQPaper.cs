using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    [Table("MCQPaper")]
    public class MCQPaper : Paper
    {
        public MCQPaper()
        {
            Questions = new List<MCQQuestion>();
        }
        public List<MCQQuestion> Questions { get; set; }
    }
}
