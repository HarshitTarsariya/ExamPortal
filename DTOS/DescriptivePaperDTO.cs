using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExamPortal.DTOS
{
    public class DescriptivePaperDTO : PaperDTO
    {
        [Required(ErrorMessage = "please upload pdf file of paper")]
        public IFormFile paper { get; set; }
        public string PaperPdfUrl { get; set; }
        

    }
}
