using System.ComponentModel.DataAnnotations;

namespace ExamPortal.Models
{
    public class MCQOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        [Required] //<======= Forces Cascade delete
        public int MCQQuestionId { get; set; }
        public MCQQuestion MCQQuestion { get; set; }
    }
}
