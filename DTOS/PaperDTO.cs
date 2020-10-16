using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.DTOS
{
    public class PaperDTO
    {
        public PaperDTO()
        {
            CreatedDate = DateTime.Now;
            DeadLine = DateTime.Now.AddDays(2);
        }
        public string PaperCode { get; set; }
        public string TeacherEmailId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string PaperTitle { get; set; }
    }
}
