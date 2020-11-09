
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    [Table("AnswerSheet")]
    public class AnswerSheet
    {
        public int Id { get; set; }
        public string StudentEmailId { get; set; }
        public DateTime SubmittedTime { get; set; }

    }
}
