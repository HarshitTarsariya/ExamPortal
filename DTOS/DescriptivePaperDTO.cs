using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExamPortal.DTOS
{
    public class DescriptivePaperDTO : PaperDTO
    {
        [Required(ErrorMessage = "please upload pdf file of paper")]
        public IFormFile paper { get; set; }
        public string PaperPdfUrl { get; set; }
        [Required(ErrorMessage = "please enter total marks")]
        [Range(minimum: 10, maximum: 1000, ErrorMessage = "Marks must be in range of 10 to 1000")]
        public int TotalMarks { get; set; }

    }
}
