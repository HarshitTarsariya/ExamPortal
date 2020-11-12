using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExamPortal.DTOS
{
    public class DescriptiveAnswerSheetDTO : AnswerSheetDTO
    {
        public string AnswerLink { get; set; }
        public int? MarksObtained { get; set; }
        [Required(ErrorMessage = "please upload answersheet")]

        public IFormFile AnswerSheet { get; set; }
        public DescriptivePaperDTO Paper { get; set; }
    }
}
