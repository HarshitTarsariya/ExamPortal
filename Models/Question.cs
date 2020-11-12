using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    [Table("Questions")]
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int Marks { get; set; }
    }
}
