using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.Models
{
    public class MCQQuestion : Question
    {
        public MCQQuestion()
        {
            MCQOptions = new List<MCQOption>();
            MCQPaper = new MCQPaper();
            TrueAnswer = new MCQOption();
        }
        [Required]
        public int MCQPaperId { get; set; }
        public MCQPaper MCQPaper { get; set; }

        public int? MCQOptionId { get; set; }
        [ForeignKey("MCQOptionId")]
        public MCQOption TrueAnswer { get; set; }

        public ICollection<MCQOption> MCQOptions { get; set; }
    }

}
