using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    [Table("MCQAnswerSheet")]
    public class MCQAnswerSheet : AnswerSheet
    {
        [Required] //<======= Forces Cascade delete
        public int MCQPaperId { get; set; }
        public MCQPaper MCQPaper { get; set; }
    }
}
