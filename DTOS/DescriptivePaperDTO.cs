using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{
    public class DescriptivePaperDTO : PaperDTO
    {
        [Required(ErrorMessage ="please upload pdf file of paper")]
        public IFormFile paper { get; set; }
    }
}
