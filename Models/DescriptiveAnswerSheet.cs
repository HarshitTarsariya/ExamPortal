using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    [Table("DescriptiveAnswerSheet")]
    public class DescriptiveAnswerSheet : AnswerSheet
    {
        public string AnswerLink { get; set; }
        public int? MarksObtained { get; set; }
        [Required] //<======= Forces Cascade delete
        public int DescriptivePaperId { get; set; }
        public DescriptivePaper DescriptivePaper { get; set; }
    }
}
