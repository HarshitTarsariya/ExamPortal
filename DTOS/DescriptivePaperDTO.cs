using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{
    public class DescriptivePaperDTO:PaperDTO
    {
        public IFormFile paper { get; set; }
    }
}
